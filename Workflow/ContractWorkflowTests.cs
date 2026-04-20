using System;
using System.Collections.Generic;
using System.Text;
using TechMoveApp.Models.Enums;
using TechMoveApp.Services;
using TechMoveApp.Models;


namespace TechMoveApp.Tests.Workflow
{
    public class ContractWorkflowTests
    {
        private readonly ContractWorkflowService _service;

        public ContractWorkflowTests()
        {
            _service = new ContractWorkflowService();
        }

        [Fact]
        public void Cannot_Create_Request_When_Contract_Is_Expired()
        {
            var contract = new Contract
            {
                Status = ContractStatus.Expired
            };

            var result = _service.CanCreateServiceRequest(contract);

            Assert.False(result);
        }

        [Fact]
        public void Cannot_Create_Request_When_Contract_Is_OnHold()
        {
            var contract = new Contract
            {
                Status = ContractStatus.OnHold
            };

            var result = _service.CanCreateServiceRequest(contract);

            Assert.False(result);
        }

        [Fact]
        public void Can_Create_Request_When_Contract_Is_Active()
        {
            var contract = new Contract
            {
                Status = ContractStatus.Active
            };

            var result = _service.CanCreateServiceRequest(contract);

            Assert.True(result);
        }
    }
}

