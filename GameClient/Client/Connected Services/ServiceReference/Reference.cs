﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserExistsFault", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
    [System.SerializableAttribute()]
    public partial class UserExistsFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserFaultException", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
    [System.SerializableAttribute()]
    public partial class UserFaultException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlayerDTO", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
    [System.SerializableAttribute()]
    public partial class PlayerDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GamesLostField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GamesTieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GamesWonField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GamesLost {
            get {
                return this.GamesLostField;
            }
            set {
                if ((this.GamesLostField.Equals(value) != true)) {
                    this.GamesLostField = value;
                    this.RaisePropertyChanged("GamesLost");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GamesTie {
            get {
                return this.GamesTieField;
            }
            set {
                if ((this.GamesTieField.Equals(value) != true)) {
                    this.GamesTieField = value;
                    this.RaisePropertyChanged("GamesTie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GamesWon {
            get {
                return this.GamesWonField;
            }
            set {
                if ((this.GamesWonField.Equals(value) != true)) {
                    this.GamesWonField = value;
                    this.RaisePropertyChanged("GamesWon");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GameDTO", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
    [System.SerializableAttribute()]
    public partial class GameDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GameIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserName1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserName2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WinnerField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GameID {
            get {
                return this.GameIDField;
            }
            set {
                if ((this.GameIDField.Equals(value) != true)) {
                    this.GameIDField = value;
                    this.RaisePropertyChanged("GameID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName1 {
            get {
                return this.UserName1Field;
            }
            set {
                if ((object.ReferenceEquals(this.UserName1Field, value) != true)) {
                    this.UserName1Field = value;
                    this.RaisePropertyChanged("UserName1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName2 {
            get {
                return this.UserName2Field;
            }
            set {
                if ((object.ReferenceEquals(this.UserName2Field, value) != true)) {
                    this.UserName2Field = value;
                    this.RaisePropertyChanged("UserName2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Winner {
            get {
                return this.WinnerField;
            }
            set {
                if ((object.ReferenceEquals(this.WinnerField, value) != true)) {
                    this.WinnerField = value;
                    this.RaisePropertyChanged("Winner");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IGameService", CallbackContract=typeof(Client.ServiceReference.IGameServiceCallback))]
    public interface IGameService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ClientConnected", ReplyAction="http://tempuri.org/IGameService/ClientConnectedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserExistsFault), Action="http://tempuri.org/IGameService/ClientConnectedUserExistsFaultFault", Name="UserExistsFault", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        void ClientConnected(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ClientConnected", ReplyAction="http://tempuri.org/IGameService/ClientConnectedResponse")]
        System.Threading.Tasks.Task ClientConnectedAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ClientDisconnected", ReplyAction="http://tempuri.org/IGameService/ClientDisconnectedResponse")]
        void ClientDisconnected(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ClientDisconnected", ReplyAction="http://tempuri.org/IGameService/ClientDisconnectedResponse")]
        System.Threading.Tasks.Task ClientDisconnectedAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/RegisterClient", ReplyAction="http://tempuri.org/IGameService/RegisterClientResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserExistsFault), Action="http://tempuri.org/IGameService/RegisterClientUserExistsFaultFault", Name="UserExistsFault", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserFaultException), Action="http://tempuri.org/IGameService/RegisterClientUserFaultExceptionFault", Name="UserFaultException", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        void RegisterClient(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/RegisterClient", ReplyAction="http://tempuri.org/IGameService/RegisterClientResponse")]
        System.Threading.Tasks.Task RegisterClientAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetPlayerDetailes", ReplyAction="http://tempuri.org/IGameService/GetPlayerDetailesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserExistsFault), Action="http://tempuri.org/IGameService/GetPlayerDetailesUserExistsFaultFault", Name="UserExistsFault", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        Client.ServiceReference.PlayerDTO GetPlayerDetailes(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetPlayerDetailes", ReplyAction="http://tempuri.org/IGameService/GetPlayerDetailesResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference.PlayerDTO> GetPlayerDetailesAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetAllGamesPlayed", ReplyAction="http://tempuri.org/IGameService/GetAllGamesPlayedResponse")]
        System.Collections.Generic.List<Client.ServiceReference.GameDTO> GetAllGamesPlayed();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetAllGamesPlayed", ReplyAction="http://tempuri.org/IGameService/GetAllGamesPlayedResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference.GameDTO>> GetAllGamesPlayedAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetAllPlayers", ReplyAction="http://tempuri.org/IGameService/GetAllPlayersResponse")]
        System.Collections.Generic.List<Client.ServiceReference.PlayerDTO> GetAllPlayers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/GetAllPlayers", ReplyAction="http://tempuri.org/IGameService/GetAllPlayersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference.PlayerDTO>> GetAllPlayersAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/SendMessage")]
        void SendMessage(string message, string fromClient, string toClient);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string message, string fromClient, string toClient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ChangeClientPassword", ReplyAction="http://tempuri.org/IGameService/ChangeClientPasswordResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserFaultException), Action="http://tempuri.org/IGameService/ChangeClientPasswordUserFaultExceptionFault", Name="UserFaultException", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        void ChangeClientPassword(string userName, string oldPassword, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/ChangeClientPassword", ReplyAction="http://tempuri.org/IGameService/ChangeClientPasswordResponse")]
        System.Threading.Tasks.Task ChangeClientPasswordAsync(string userName, string oldPassword, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DeleteAccount", ReplyAction="http://tempuri.org/IGameService/DeleteAccountResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Client.ServiceReference.UserFaultException), Action="http://tempuri.org/IGameService/DeleteAccountUserFaultExceptionFault", Name="UserFaultException", Namespace="http://schemas.datacontract.org/2004/07/GamesServer")]
        void DeleteAccount(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGameService/DeleteAccount", ReplyAction="http://tempuri.org/IGameService/DeleteAccountResponse")]
        System.Threading.Tasks.Task DeleteAccountAsync(string userName, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/UpdateClientsList")]
        void UpdateClientsList(System.Collections.Generic.List<string> clients);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGameService/SendMessageToClient")]
        void SendMessageToClient(string message, string fromClient);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGameServiceChannel : Client.ServiceReference.IGameService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GameServiceClient : System.ServiceModel.DuplexClientBase<Client.ServiceReference.IGameService>, Client.ServiceReference.IGameService {
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GameServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ClientConnected(string username, string password) {
            base.Channel.ClientConnected(username, password);
        }
        
        public System.Threading.Tasks.Task ClientConnectedAsync(string username, string password) {
            return base.Channel.ClientConnectedAsync(username, password);
        }
        
        public void ClientDisconnected(string username) {
            base.Channel.ClientDisconnected(username);
        }
        
        public System.Threading.Tasks.Task ClientDisconnectedAsync(string username) {
            return base.Channel.ClientDisconnectedAsync(username);
        }
        
        public void RegisterClient(string username, string password) {
            base.Channel.RegisterClient(username, password);
        }
        
        public System.Threading.Tasks.Task RegisterClientAsync(string username, string password) {
            return base.Channel.RegisterClientAsync(username, password);
        }
        
        public Client.ServiceReference.PlayerDTO GetPlayerDetailes(string username) {
            return base.Channel.GetPlayerDetailes(username);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference.PlayerDTO> GetPlayerDetailesAsync(string username) {
            return base.Channel.GetPlayerDetailesAsync(username);
        }
        
        public System.Collections.Generic.List<Client.ServiceReference.GameDTO> GetAllGamesPlayed() {
            return base.Channel.GetAllGamesPlayed();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference.GameDTO>> GetAllGamesPlayedAsync() {
            return base.Channel.GetAllGamesPlayedAsync();
        }
        
        public System.Collections.Generic.List<Client.ServiceReference.PlayerDTO> GetAllPlayers() {
            return base.Channel.GetAllPlayers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Client.ServiceReference.PlayerDTO>> GetAllPlayersAsync() {
            return base.Channel.GetAllPlayersAsync();
        }
        
        public void SendMessage(string message, string fromClient, string toClient) {
            base.Channel.SendMessage(message, fromClient, toClient);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string message, string fromClient, string toClient) {
            return base.Channel.SendMessageAsync(message, fromClient, toClient);
        }
        
        public void ChangeClientPassword(string userName, string oldPassword, string newPassword) {
            base.Channel.ChangeClientPassword(userName, oldPassword, newPassword);
        }
        
        public System.Threading.Tasks.Task ChangeClientPasswordAsync(string userName, string oldPassword, string newPassword) {
            return base.Channel.ChangeClientPasswordAsync(userName, oldPassword, newPassword);
        }
        
        public void DeleteAccount(string userName, string password) {
            base.Channel.DeleteAccount(userName, password);
        }
        
        public System.Threading.Tasks.Task DeleteAccountAsync(string userName, string password) {
            return base.Channel.DeleteAccountAsync(userName, password);
        }
    }
}
