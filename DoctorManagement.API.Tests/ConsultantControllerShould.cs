using DoctorManagement.API.Controllers;
using DoctorManagement.API.DTOs;
using DoctorManagement.Domain.Aggregates.DoctorAggregate;
using DoctorManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoctorManagement.API.Tests
{
    [TestFixture]
    public class ConsultantControllerShould
    {
        [Test]
        public async Task SubmitDiagnosisTreatment_ReturnStatusCode200()
        {
            var report = new ConsultantDTO()
            {
                Id = 1,
                PatientId = 101,
                DoctorID = 1001,
                Diagnosis = "This is Diagnosis",
                Treatment = "This is Treatment",
                Prescription = "This is Prescription",
                Problem = "This is Problem"
            };
            var repo = new Mock<IRepository<Consultant>>();
            repo.Setup(m => m.SaveAsync());
            var repoObj = repo.Object;

            var controller = new ConsultantController(repoObj);

            StatusCodeResult result = (StatusCodeResult)await controller.SubmitDiagnosisTreatment(report).ConfigureAwait(false);
            Assert.That(result.StatusCode, Is.EqualTo(201));
        }
    }
}
