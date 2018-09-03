using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace GamesServer {

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GameService : IGameService {

        SortedDictionary<string, IGameServiceCallback> callbacks
            = new SortedDictionary<string, IGameServiceCallback>();
        public void ClientConnected(string username, string password) {
            if (callbacks.ContainsKey(username)) {
                throw new FaultException<UserExistsFault>(
                    new UserExistsFault {
                        Message = username + " already Loged in"
                    });
            }
            else {
                try {
                    Player player = GetPlayerFromDB(username);
                    if (player.Password != password) {
                        throw new FaultException("Incorrect Password, Try again");
                    }
                    IGameServiceCallback callback =
                    OperationContext.Current.GetCallbackChannel<IGameServiceCallback>();
                    callbacks.Add(username, callback);
                    UpdateUsersList();
                } catch (Exception ex) {
                    throw new FaultException(ex.Message);
                }
            }
        }

        private void UpdateUsersList() {
            Thread updateListThread = new Thread(() => {
                foreach (var callback in callbacks.Values) {
                    callback.UpdateClientsList(callbacks.Keys);
                }
            });
            updateListThread.Start();
        }

        public void ClientDisconnected(string username) {
            callbacks.Remove(username);
            UpdateUsersList();
        }


        public void RegisterClient(string username, string password) {
            if (callbacks.ContainsKey(username)) {
                throw new FaultException<UserExistsFault>(
                    new UserExistsFault {
                        Message = username + " User name is already exist!"
                    });
            }
            try {
                RegisterNewPlayer(username, password);
                IGameServiceCallback callback =
                    OperationContext.Current.GetCallbackChannel
                    <IGameServiceCallback>();
                callbacks.Add(username, callback);
                UpdateUsersList();
            } catch (Exception ex) {
                throw new FaultException(ex.Message);
            }
        }

        public PlayerDTO GetPlayerDetailes(string username) {
            return new PlayerDTO(GetPlayerFromDB(username));
        }

        private void RegisterNewPlayer(string username, string password) {
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities()) {
                ctx.Players.Add(new Player() {
                    UserName = username,
                    Password = password
                });
                try {
                    ctx.SaveChanges();
                } catch (Exception) {
                    throw new FaultException<UserExistsFault>(
                    new UserExistsFault {
                        Message = username + " already exists\n"
                    });
                }
            }
        }

        private Player GetPlayerFromDB(string username) {
            Player player = null;
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities()) {
                try {
                    player = (from p in ctx.Players
                              where p.UserName == username
                              select p).First();
                } catch (Exception) {
                    throw new FaultException("Such user dosen\'t seem to exist");
                }
            }
            return player;
        }

        public List<GameDTO> GetAllGamesPlayed() {
            List<GameDTO> games = null;
            using (var ctx = new minesweeper_ShlomiOhana_YardenDananEntities()) {
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

            foreach(Player player in players)
            {
                toRet.Add(new PlayerDTO(player));
            }
            return toRet;
        }
    }
}
