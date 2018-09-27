using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace GamesServer
{
    public enum GameResult
    {
        Lose = 0,
        Win = 1,
        Tie
    }
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : IGameService
    {

        SortedDictionary<string, IGameServiceCallback> callbacks
            = new SortedDictionary<string, IGameServiceCallback>();
        List<LiveMatch> LiveMatches = new List<LiveMatch>();
        public void ClientConnected(string username, string password)
        {
            if (callbacks.ContainsKey(username))
            {
                throw new FaultException<UserExistsFault>(
                    new UserExistsFault
                    {
                        Message = username + " already Loged in"
                    });
            }
            else
            {
                try
                {
                    Player player = GetPlayerFromDB(username);
                    if (player.Password != password)
                    {
                        throw new FaultException("Incorrect Password, Try again");
                    }
                    IGameServiceCallback callback =
                    OperationContext.Current.GetCallbackChannel<IGameServiceCallback>();
                    callbacks.Add(username, callback);
                    UpdateUsersList();
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }

        private void UpdateUsersList()
        {
            Thread updateListThread = new Thread(() =>
            {
                foreach (var callback in callbacks.Values)
                {
                    callback.UpdateClientsList(callbacks.Keys);
                }
            });
            updateListThread.Start();
        }

        public void ClientDisconnected(string username)
        {
            callbacks.Remove(username);
            UpdateUsersList();
        }


        public void RegisterClient(string username, string password)
        {
            if (callbacks.ContainsKey(username))
            {
                throw new FaultException<UserExistsFault>(
                    new UserExistsFault
                    {
                        Message = username + " User name is already exist!"
                    });
            }
            try
            {
                RegisterNewPlayer(username, password);
                IGameServiceCallback callback =
                    OperationContext.Current.GetCallbackChannel
                    <IGameServiceCallback>();
                callbacks.Add(username, callback);
                UpdateUsersList();
            }
            catch(FaultException<UserFaultException> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public PlayerDTO GetPlayerDetailes(string username)
        {
            return new PlayerDTO(GetPlayerFromDB(username));
        }

        private void RegisterNewPlayer(string username, string password)
        {
            if (!IsValidName(username))
            {
                throw new FaultException<UserFaultException>(
                 new UserFaultException(), new FaultReason("User Name can contains only letters and numbers, 3-15 characters."));
            }

            if (!IsValidPassword(password))
            {
                throw new FaultException<UserFaultException>(
                new UserFaultException(), new FaultReason("Password can contains only letters and numbers, 6-15 characters."));
            }
            
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                ctx.Players.Add(new Player()
                {
                    UserName = username,
                    Password = password
                });
                try
                {
                    ctx.SaveChanges();
                }
                catch (Exception)
                {
                    throw new FaultException<UserExistsFault>(
                    new UserExistsFault
                    {
                        Message = username + " already exists\n"
                    });
                }
            }
        }

        private Player GetPlayerFromDB(string username)
        {
            Player player = null;
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                try
                {
                    player = (from p in ctx.Players
                              where p.UserName == username
                              select p).First();
                }
                catch (Exception)
                {
                    throw new FaultException("Such user dosen\'t seem to exist");
                }
            }
            return player;
        }

        public List<GameDTO> GetAllGamesPlayed()
        {
            List<GameDTO> games = null;
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                var games_db = from g in ctx.Games select new GameDTO(g);
                games = games_db.ToList();
            }
            return games;
        }

        public List<PlayerDTO> GetAllPlayers()
        {
            List<PlayerDTO> toRet = new List<PlayerDTO>();
            List<Player> players = new List<Player>();
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                players = (from p in ctx.Players
                           select p).ToList();
            }

            foreach (Player player in players)
            {
                toRet.Add(new PlayerDTO(player));
            }
            return toRet;
        }

        public void SendMessage(string message, string fromClient, string toClient)
        {
            if (callbacks.ContainsKey(toClient))
            {
                Thread sendThread = new Thread(() =>
                        callbacks[toClient].SendMessageToClient(message, fromClient));
                sendThread.Start();
            }
        }

        public void ChangeClientPassword(string userName, string oldPassword, string newPassword)
        {
            if (!IsValidPassword(newPassword))
            {
                throw new FaultException<UserFaultException>(
                new UserFaultException(), new FaultReason("Password can contains only letters and numbers, 6-15 characters."));
            }
            Player passClient = new Player
            {
                UserName = userName,
                Password = oldPassword
            };

            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                try
                {
                    var passChange = (from c in ctx.Players
                                      where c.UserName == passClient.UserName && c.Password == passClient.Password
                                      select c).FirstOrDefault();
                    if (passChange == null) throw new Exception();
                    else passChange.Password = newPassword;
                    ctx.SaveChanges();
                }
                catch (Exception)
                {
                    UserFaultException fault = new UserFaultException()
                    { Message = "Password change failed" };
                    throw new FaultException<UserFaultException>(fault);
                }
            }
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z0-9]{6,15}$");
        }

        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[A-Za-z0-9]{3,15}$");
        }

        public void DeleteAccount(string userName, string password)
        {
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                Player playerToRemove = ctx.Players.Where
                    (p => p.UserName.Equals(userName) &&
                     p.Password.Equals(password)).FirstOrDefault();
                if(playerToRemove != null)
                {
                    ctx.Players.Remove(playerToRemove);
                    ctx.SaveChanges();
                    UpdateUsersList();
                }
                else
                {
                    throw new FaultException<UserFaultException>(
                    new UserFaultException(), new FaultReason("Password is Incorrect"));
                }
            }
        }

        public LiveMatch GetRandomGrid(int rows, int columns, int mines)
        {
            MinesweeperGrid randomGrid =  new MinesweeperGrid(rows, columns, mines);
            LiveMatch match = new LiveMatch();
            match.Board = randomGrid;
            return match;
        }

        public void SendInvitation(string senderName, string reciverName, GameParams parameters, PlayerStats playerStats)
        {
            if (callbacks.ContainsKey(reciverName))
            {
                Thread sendThread = new Thread(() => callbacks[reciverName].ShowSentInvitation(senderName, parameters, playerStats));
                sendThread.Start();
            }
            else
            {
                throw new FaultException<UserFaultException>(
                   new UserFaultException(), new FaultReason("Not found such user."));
            }
        }

        public void CancelInvitation(string senderName)
        {
            if (callbacks.ContainsKey(senderName))
            {
                Thread sendThread = new Thread(() => callbacks[senderName].CancelSenderInvitation());
                sendThread.Start();
            }
            else
            {
                throw new FaultException<UserFaultException>(
                   new UserFaultException(), new FaultReason("Not found such user."));
            }
        }

        public void AcceptInvitation(string senderName, string reciverName, GameParams gameParams)
        {
            if (callbacks.ContainsKey(senderName))
            {
                LiveMatch newMatch = new LiveMatch();
                newMatch.GameParams = new GameParams(gameParams.rows, gameParams.cols, gameParams.mines);
                newMatch.Board = new MinesweeperGrid(gameParams.rows, gameParams.cols, gameParams.mines);
                newMatch.HomePlayer = senderName;
                newMatch.AwayPlayer = reciverName;
                newMatch.WhosTurn = true;
                LiveMatches.Add(newMatch);
                Thread sendThread = new Thread(() => callbacks[senderName].AcceptSenderInvitation(newMatch));
                sendThread.Start();
            }
            else
            {
                throw new FaultException<UserFaultException>(
                   new UserFaultException(), new FaultReason("Not found such user."));
            }
        }

        public LiveMatch GetSameGridAsOpponent(string senderName, string reciverName)
        {
            return FindMatchByPlayerNames(senderName, reciverName);
        }

        public bool WhosTurn(string homePlayer, string awayPlayer)
        {
            LiveMatch match = LiveMatches.Find(m => (m.HomePlayer == homePlayer)
            && (m.AwayPlayer == awayPlayer));
            if (match != null)
            {
                return match.WhosTurn;
            }
            else
            {
                throw new FaultException<UserFaultException>(
                  new UserFaultException(), new FaultReason("Something went wrong, match not found."));
            }
        }

        public void FinishTurn(MinesweeperItemCellDefinition cell, string homePlayer,string awayPlayer, string playerName)
        {
            LiveMatch serverMatch = FindMatchByPlayerNames(homePlayer, awayPlayer);
            MinesweeperItem item = serverMatch.Board.FindItemAt(cell);
            serverMatch.Board.EvaluateItem(item);
            string playerToNotfiy = playerName.Equals(serverMatch.HomePlayer) ?
                serverMatch.AwayPlayer : serverMatch.HomePlayer;

            if (callbacks.ContainsKey(playerToNotfiy))
            {
                Thread sendThread = new Thread(() => callbacks[playerToNotfiy].UpdateOpponentBoard(cell));
                sendThread.Start();
            }
            else
            {
                throw new FaultException<UserFaultException>(
                   new UserFaultException(), new FaultReason("Opponent not found."));
            }
            serverMatch.WhosTurn = !serverMatch.WhosTurn;

        }

        private LiveMatch FindMatchByPlayerNames(string homePlayer, string awayPlayer)
        {
            LiveMatch match = LiveMatches.Find(m => (m.HomePlayer == homePlayer)
                              && (m.AwayPlayer == awayPlayer));
            if (match != null)
            {
                return match;
            }
            else
            {
                throw new FaultException<UserFaultException>(
                  new UserFaultException(), new FaultReason("Something went wrong, match not found."));
            }
        }

        public void PlayerLose(string homePlayer, string awayPlayer, string loser)
        {
            LiveMatch match = FindMatchByPlayerNames(homePlayer, awayPlayer);
            string playerToNotify = homePlayer.Equals(loser) ? awayPlayer : homePlayer;
            if (callbacks.ContainsKey(playerToNotify))
            {
                Thread notifyWinner = new Thread(() => callbacks[playerToNotify].NotifyWinner());
                notifyWinner.Start();
            }
            UpdatePlayerStats(playerToNotify, GameResult.Win);
            UpdatePlayerStats(loser, GameResult.Lose);
            SaveGameToDB(match, playerToNotify);
        }

        private void UpdatePlayerStats(string playerName, GameResult result)
        {
            Player player = GetPlayerFromDB(playerName);

                switch (result)
            {
                case GameResult.Lose:
                    player.GamesLost++;
                    break;
                case GameResult.Win:
                    player.GamesWon++;
                    break;
                case GameResult.Tie:
                    player.GamesTie++;
                    break;
                default:
                    break;
            }
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                ctx.Entry(player).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        private void SaveGameToDB(LiveMatch match,string winnerName)
        {
            Game game = new Game();
            game.Winner = winnerName;
            game.Player1_UserName = match.HomePlayer;
            game.Player2_UserName = match.AwayPlayer;
            game.Date =  DateTime.Now;
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities())
            {
                ctx.Games.Add(game);
                ctx.SaveChanges();
            }
            LiveMatches.Remove(match);
        }

        public void GameFinishInTie(string homePlayer, string awayPlayer)
        {
            LiveMatch match = FindMatchByPlayerNames(homePlayer, awayPlayer);
            if (callbacks.ContainsKey(homePlayer))
            {
                Thread notifyWinner = new Thread(() => callbacks[homePlayer].NotifyTie());
                notifyWinner.Start();
            }
            if (callbacks.ContainsKey(awayPlayer))
            {
                Thread notifyWinner = new Thread(() => callbacks[awayPlayer].NotifyTie());
                notifyWinner.Start();
            }

            UpdatePlayerStats(homePlayer, GameResult.Tie);
            UpdatePlayerStats(awayPlayer, GameResult.Tie);
            SaveGameToDB(match, "Tie");
        }
    }
}
