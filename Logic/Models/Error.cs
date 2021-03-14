using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces;

namespace Logic.Models
{
    public class Error : ICombinable<Error>
    {
        public HashSet<string> Messages { get; set; }

        public Error()
        {
            Messages = new HashSet<string>();
        }
        
        public Error(string message)
        {
            Messages = new HashSet<string> {message};
        }

        private Error(IEnumerable<string> messages)
        {
            Messages = messages.ToHashSet();
        }

        public Error Combine(Error other)
        {
            return new Error(Messages.Concat(other.Messages).GroupBy(x => x).Select(x => x.First()));
        }
    }
}