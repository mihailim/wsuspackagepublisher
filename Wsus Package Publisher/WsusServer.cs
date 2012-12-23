using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class WsusServer
    {
        private string _name = "";
        private bool _isLocal = false;
        private int _port = 80;
        private bool _useSSL = false;
        private int _deadLineDaysSpan = 7;
        private int _deadLineHour = 16;
        private int _deadLineMinute = 31;
        private List<MetaGroup> _metaGroups = new List<MetaGroup>();

        internal WsusServer()
        { }

        internal WsusServer(string name, bool isLocal, int port, bool useSSL, int deadLineDaysSpan, int hour, int minute)
        {
            Name = name;
            IsLocal = isLocal;
            Port = port;
            UseSSL = useSSL;
            DeadLineDaysSpan = deadLineDaysSpan;
            DeadLineHour = hour;
            DeadLineMinute = minute;
        }

        /// <summary>
        /// Get or Set the name of the server.
        /// </summary>
        internal string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _name = value;
            }
        }

        /// <summary>
        /// Get or Set if the application should connect to the local machine.
        /// </summary>
        internal bool IsLocal
        {
            get { return _isLocal; }
            set { _isLocal = value; }
        }

        /// <summary>
        /// Get or Set communication port for this server.
        /// </summary>
        internal int Port
        {
            get { return _port; }
            set
            {
                if (value > 0 && value <= 65535)
                    _port = value;
            }
        }

        /// <summary>
        /// Get or Set if the communication use SSL.
        /// </summary>
        internal bool UseSSL
        {
            get { return _useSSL; }
            set { _useSSL = value; }
        }

        /// <summary>
        /// Get or Set the number of days betwwen today and the DeadLine for udpate installation.
        /// </summary>
        internal int DeadLineDaysSpan
        {
            get { return _deadLineDaysSpan; }
            set
            {
                if (value >= 0 && value <= 365)
                    _deadLineDaysSpan = value;
            }
        }

        /// <summary>
        /// Get or Set the hour of the DeadLine.
        /// </summary>
        internal int DeadLineHour
        {
            get { return _deadLineHour; }
            set
            {
                if (value >= 0 && value <= 23)
                    _deadLineHour = value;
            }
        }

        internal int DeadLineMinute
        {
            get { return _deadLineMinute; }
            set
            {
                if (value >= 0 && value <= 59)
                    _deadLineMinute = value;
            }
        }

        internal List<MetaGroup> MetaGroups
        {
            get { return _metaGroups; }
        }

        internal bool IsValid()
        {
            return (!String.IsNullOrEmpty(Name) && Port > 0 && Port < 65536 &&
                DeadLineDaysSpan >= 0 && DeadLineDaysSpan <= 365 &&
                DeadLineHour >= 0 && DeadLineHour <= 23 &&
                DeadLineMinute >= 0 && DeadLineMinute <= 59);
        }

        public override string ToString()
        {
            if (IsLocal)
                return Name + " (Local)";
            else
                return Name + " (" + Port.ToString() + ")";
        }

    }
}
