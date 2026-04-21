using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TechMoveApp.Services;

namespace TechMoveApp.Tests.Services
{
    public class FileServiceTests
    {
        [Fact]
        public async Task Upload_NonPdf_Should_Fail()
        {
            var envMock = new Mock<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns("wwwroot");

            var service = new FileService(envMock.Object);

            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.exe");
            fileMock.Setup(f => f.Length).Returns(100);

            var result = await service.SavePdfAsync(fileMock.Object);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Upload_Pdf_Should_Succeed()
        {
            var envMock = new Mock<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns("wwwroot");

            var service = new FileService(envMock.Object);

            var fileMock = new Mock<IFormFile>();
            var content = "Dummy PDF content";
            var fileName = "test.pdf";

            var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));

            fileMock.Setup(f => f.OpenReadStream()).Returns(ms);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(ms.Length);
            fileMock.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), default))
                .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream));

            var result = await service.SavePdfAsync(fileMock.Object);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task Upload_NullFile_Should_Fail()
        {
            var envMock = new Mock<IWebHostEnvironment>();
            envMock.Setup(e => e.WebRootPath).Returns("wwwroot");

            var service = new FileService(envMock.Object);

            var result = await service.SavePdfAsync(null);

            Assert.False(result.Success);
        }
    }
}

