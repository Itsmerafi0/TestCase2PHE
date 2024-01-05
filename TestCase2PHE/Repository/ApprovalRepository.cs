﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestCase2PHE.Contracts;
using TestCase2PHE.Data;
using TestCase2PHE.Models;

namespace TestCase2PHE.Repository
{
    public class ApprovalRepository : GeneralRepository<Approval>, IApprovalRepository
    {
        public ApprovalRepository(PHEDbContext context) : base (context) { }
    }
}