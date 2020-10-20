using System;

namespace BolaoTI.Dominio
{
    public class Claim
    {
        private int _id;
        private Guid _userId;
        private string _type;
        private string _value;
        private Usuario _usuario;
        
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        
        public Usuario Usuario
        {
            get
            {
                return _usuario;
            }
            set { 
                _usuario = value;
                _userId = value.Id;
            }
        }
    }
}
