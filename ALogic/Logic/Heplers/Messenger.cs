using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALogic.Logic.Heplers
{
    public static class Messenger
    {
        public static event EventHandler ActionMessage;
        public static event EventHandler ActionProgress;

        public static void Send(string message, int progress)
        {
            if (ActionMessage != null)
                ActionMessage(message, null);
            if (ActionProgress != null)
                ActionProgress(progress, null);
        }
    }
}
