using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeNotesClient.Command
{
    public interface Command
    {
        void Execute();
    }
}
