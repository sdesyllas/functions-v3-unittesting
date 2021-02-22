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
            Mock<IContext> dbContext = new Mock<IContext>();
            Mock<ITopicService> topicService = new Mock<ITopicService>();
            var record = Builder<FunctionUnitTesting.Domain.Record>.CreateNew().
                With(x => x.Id = "1").With(x => x.Name = "Name").Build();
            dbContext.Setup(x => x.GetRecordById(It.IsAny<string>())).ReturnsAsync(record);
            topicService.Setup(x => x.SendMessage(It.IsAny<string>())).Verifiable();
            var logger = TestFactory.CreateLogger();
            var request = TestFactory.CreateHttpRequest();
            RecordFunction recordFunction = new RecordFunction(dbContext.Object, topicService.Object);
            
            // Act
            var response = (OkObjectResult)await recordFunction.Run(request, "1", logger);

            // Assert
            dbContext.Verify(x => x.GetRecordById("1"), Times.Once);
            topicService.Verify(x => x.SendMessage("1"), Times.Once);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal("1", ((FunctionUnitTesting.Domain.Record)response.Value).Id);
            Assert.Equal("Name", ((FunctionUnitTesting.Domain.Record)response.Value).Name);
        }

        [Fact]
        public async void Http_trigger_Get_should_return_not_found_for_notexisting_record_id()
        {
            // Arrange

            
            // Act
            

            // Assert
            
        }
    }
}
