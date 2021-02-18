using FizzWare.NBuilder;
using FunctionUnitTesting;
using FunctionUnitTesting.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace XUnitTestProject
{
    public class RecordFunctionTests
    {
        [Fact]
        public async void Http_trigger_Get_should_return_known_record_for_given_id()
        {
            // Arrange
            Mock<IDbContext> idbContext = new Mock<IDbContext>();
            var record = Builder<FunctionUnitTesting.Domain.Record>.CreateNew().
                With(x => x.Id = "1").With(x => x.Name = "Name").Build();
            idbContext.Setup(x => x.GetRecordById(It.IsAny<string>())).ReturnsAsync(record);
            var logger = TestFactory.CreateLogger();
            var request = TestFactory.CreateHttpRequest();
            RecordFunction recordFunction = new RecordFunction(idbContext.Object);

            // Act
            var response = (OkObjectResult)await recordFunction.Run(request, "1", logger);

            // Assert
            idbContext.Verify(x => x.GetRecordById("1"), Times.Once);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("1", ((FunctionUnitTesting.Domain.Record)response.Value).Id);
            Assert.Equal("Name", ((FunctionUnitTesting.Domain.Record)response.Value).Name);
        }

        [Fact]
        public async void Http_trigger_Get_should_return_not_found_for_notexisting_record_id()
        {
            Mock<IDbContext> idbContext = new Mock<IDbContext>();

            idbContext.Setup(x => x.GetRecordById(It.IsAny<string>())).ReturnsAsync(value: null);
            var logger = TestFactory.CreateLogger();
            var request = TestFactory.CreateHttpRequest();
            RecordFunction recordFunction = new RecordFunction(idbContext.Object);

            // Act
            var response = (ObjectResult)await recordFunction.Run(request, "11", logger);

            // Assert
            idbContext.Verify(x => x.GetRecordById("11"), Times.Once);
            Assert.Equal(404, response.StatusCode);
        }
    }
}
