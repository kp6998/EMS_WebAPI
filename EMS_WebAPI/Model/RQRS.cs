namespace EMS_WebAPI.Model
{
    public class RQRS
    {
        #region Response
        public class ResponseData
        {
            public string strStatus { get; set; }
            public string strResponse { get; set; }
        };
        #endregion

        #region Request
        public class State
        {
            public string stateCode { get; set; }
            public string stateName { get; set; }
            public int numberOfMPSeats { get; set; }
        }

        public class Party
        {
            public string partyCode { get; set; }
            public string partyName { get; set; }
            public string symbol { get; set; }
        }

        public class Candidate
        {
            public int candidateId { get; set; }
            public string candidateName { get; set; }
            public string partyCode { get; set; }
            public string stateCode { get; set; }
        }

        public class Voter
        {
            public int voterId { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string photoUrl { get; set; }
            public bool isApproved { get; set; }
        }
        public class VoterId
        {
            public int voterId { get; set; }
        }

        public class Vote
        {
            public int voteId { get; set; }
            public int candidateId { get; set; }
            public int voterId { get; set; }
        }
        #endregion
    }
}
