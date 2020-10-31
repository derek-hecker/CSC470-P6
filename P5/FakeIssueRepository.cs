﻿using Castle.DynamicProxy.Contributors;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P5
{
    public class FakeIssueRepository : IIssueRepository
    {
        public const string NO_ERROR = "";
        public const string EMPTY_TITLE_ERROR = "A Title is required";
        public const string EMPTY_DISCOVERY_DATE = "Must select a Discovery Date/Time";
        public const string FUTURE_DISCOVERY_DATE = "Issues can't be fromthe future";
        public const string EMPTY_DISCOVERER_ERROR = "A discoverer is required";
        public const string DUPLICATE_TITLE_ERROR = "Issue Title must be unique. ";

        private static List<Issue> _Issues = new List<Issue>();
        public string Add(Issue issue, out int Id)
        {
            Id = 0;
            string newIssueTitle = issue.Title.Trim();
            string newIssueDiscoverer = issue.Discoverer.Trim();
            if (IsDuplicateTitle(newIssueTitle))
            {
                return DUPLICATE_TITLE_ERROR;
            }
            if (newIssueTitle == "")
            {
                return EMPTY_TITLE_ERROR;
            }
            if (newIssueDiscoverer == "")
            {
                return EMPTY_DISCOVERER_ERROR;
            }
            issue.Id = GetNextID();
            _Issues.Add(issue);
            Id = issue.Id;
            return NO_ERROR;
            
        }

        public List<Issue> GetAll(int ProjectID)
        {
            return _Issues;
        }

        public Issue GetIssueByID(int ID)
        {
            Issue tmpIssue;
            try
            {
                tmpIssue = _Issues[ID];
            }
            catch
            {
                tmpIssue = null;
            }
            return tmpIssue;
        }

        public List<string> GetIssuesByDiscoverer(int ProjectID)
        {
            throw new NotImplementedException();
        }

        public List<string> GetIssuesByMonth(int ProjectID)
        {
            throw new NotImplementedException();
        }

        public int GetTotalNumberOfIssues(int ProjectID)
        {
            throw new NotImplementedException();
        }

        public string Modify(Issue issue)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Issue issue)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicateTitle(string issueTitle)
        {
            bool isDuplicate = false;
            foreach (Issue i in _Issues)
            {
                if (issueTitle == i.Title)
                {
                    isDuplicate = true;
                }
            }
            return isDuplicate;
        }
        private int GetNextID()
        {
            int currentMaxID = 0;
            foreach (Issue i in _Issues)
            {
                currentMaxID = i.Id;
            }
            return ++currentMaxID;
        }
    }
}
