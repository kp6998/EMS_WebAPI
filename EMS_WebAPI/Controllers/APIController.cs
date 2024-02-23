using EMS_WebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;

namespace EMS_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        // Method to get MP seats list
        [HttpGet, Route("getMPSeatsList")]
        public RQRS.ResponseData GetMPSeatsList()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT * FROM States;";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to get parties list
        [HttpGet, Route("getPartiesList")]
        public RQRS.ResponseData GetPartiesList()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT * FROM Parties;";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to get voters list
        [HttpGet, Route("getVotersList")]
        public RQRS.ResponseData GetVotersList()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT * FROM Voters;";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to get Candidate list
        [HttpGet, Route("getCandidateList")]
        public RQRS.ResponseData GetCandidateList()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT * FROM Candidates;";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to get the voting system
        [HttpGet, Route("getVotingSystem")]
        public RQRS.ResponseData GetVotingSystem()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT c.CandidateId, c.CandidateName, p.PartyName, p.Symbol, p.PartyCode, s.StateName, s.StateCode " +
                              "FROM Candidates c " +
                              "INNER JOIN Parties p ON c.PartyCode = p.PartyCode " +
                              "INNER JOIN States s ON c.StateCode = s.StateCode";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to manage MP seats
        [HttpPost, Route("manageMPSeats")]
        public RQRS.ResponseData ManageMPSeats([FromBody] RQRS.State req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"UPDATE States SET NumberOfMPSeats = '{req.numberOfMPSeats}' WHERE StateCode = '{req.stateCode}'";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to register a party
        [HttpPost, Route("registerParty")]
        public RQRS.ResponseData RegisterParty(RQRS.Party req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"INSERT INTO Parties (PartyCode, PartyName, Symbol) VALUES ('{req.partyCode}', '{req.partyName}', '{req.symbol}')";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to login voter
        [HttpPost, Route("voterLogin")]
        public RQRS.ResponseData VoterLogin(RQRS.VoterId req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"SELECT VoterId FROM Voters WHERE VoterId = {req.voterId}";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to register a voter
        [HttpPost, Route("registerVoter")]
        public RQRS.ResponseData RegisterVoter(RQRS.Voter req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"INSERT INTO Voters (Name, Address, PhotoUrl, IsApproved) VALUES ('{req.name}', '{req.address}', '{req.photoUrl}', 0)";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to approve a voter
        [HttpPost, Route("approveVoter")]
        public RQRS.ResponseData ApproveVoter(RQRS.VoterId req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"UPDATE Voters SET IsApproved = 1 WHERE VoterId = {req.voterId}";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to register a candidate
        [HttpPost, Route("registerCandidate")]
        public RQRS.ResponseData RegisterCandidate(RQRS.Candidate req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"INSERT INTO Candidates (CandidateName, PartyCode, StateCode) VALUES ('{req.candidateName}', '{req.partyCode}', '{req.stateCode}')";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to check already voted list
        [HttpPost, Route("checkAlreadyVoted")]
        public RQRS.ResponseData CheckAlreadyVoted(RQRS.Vote req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"SELECT * FROM Votes where VoterId = {req.voterId};";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method for a voter to cast a vote
        [HttpPost, Route("vote")]
        public RQRS.ResponseData Vote(RQRS.Vote req)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = $"INSERT INTO Votes (CandidateId, VoterId) VALUES ({req.candidateId}, {req.voterId})";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }

        // Method to get election results
        [HttpGet, Route("getElectionResults")]
        public RQRS.ResponseData GetElectionResults()
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            string strQuery = "SELECT p.PartyCode, p.PartyName, COUNT(v.VoteId) AS NumberOfVotes " +
                              "FROM Parties p " +
                              "LEFT JOIN Candidates c ON p.PartyCode = c.PartyCode " +
                              "LEFT JOIN Votes v ON c.CandidateId = v.CandidateId " +
                              "GROUP BY p.PartyCode, p.PartyName " +
                              "ORDER BY NumberOfVotes DESC";
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, strQuery);
            response.strStatus = strStatus;
            response.strResponse = strResponse;

            return response;
        }

        // Method to execute a direct query
        [HttpPost, Route("executeQuery")]
        public RQRS.ResponseData ExecuteQuery(string query)
        {
            string strStatus = string.Empty;
            string strResponse = string.Empty;
            RQRS.ResponseData response = new RQRS.ResponseData();
            strResponse = DBHandler.ExecuteQuery(ref strStatus, query);
            response.strStatus = strStatus;
            response.strResponse = strResponse;
            return response;
        }
    }
}
