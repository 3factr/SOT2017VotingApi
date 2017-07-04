using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace SOT2017VotingApi
{
    public class VoteStorage
    {
        static volatile VoteStorage _instance;
        static readonly object LockObject = new object();
        
        public static VoteStorage Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new VoteStorage();
                        }
                    }
                }

                return _instance;
            }
        }

        readonly ConcurrentDictionary<string, int> _votes;

        private VoteStorage()
        {
            _votes = new ConcurrentDictionary<string, int>();
        }

        public bool Add(string voteOption)
        {
            if (string.IsNullOrEmpty(voteOption))
            {
                return false;;
            }
            
            Console.WriteLine("Registered vote for " + voteOption);
            return _votes.AddOrUpdate(voteOption.Trim().ToLower(), 1, (opt, voteCount) => voteCount + 1) > 0;
        }

        public IReadOnlyDictionary<string, int> GetVotes()
        {
            return new SortedDictionary<string, int>(_votes);
        }
    }
}