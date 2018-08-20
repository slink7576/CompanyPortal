using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using ApplicationCore.Services;
using ApplicationCore.Interfaces;
using ApplicationCore.Entities;
using System.Threading.Tasks;
using ApplicationCore.Exceptions;

namespace UnitTests.ApplicationCore.Services
{
    public class NewsServiceTests
    {
        private Mock<INewsRepository> _newsRepositoryMock;
        private Mock<IAsyncRepository<ChartItem>> _repositoryChart;

        private void Arrange()
        {
            _newsRepositoryMock = new Mock<INewsRepository>();
            _repositoryChart = new Mock<IAsyncRepository<ChartItem>>();
        }


        [Fact]
        public async Task CreateNewsItemTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, null);

            var testedNews = new NewsItem { Header = "Header" };
            await newsService.CreateNewsItem(testedNews);

            _newsRepositoryMock.Verify(c => c.AddAsync(It.Is<NewsItem>(arg => arg.Header == "Header")));
        }


        [Fact]
        public async Task DeleteNewsItemTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);

            var testedNews = new NewsItem { Header = "Header", Id = 1, ChartItems = new List<ChartItem> { new ChartItem() } };
           

            _newsRepositoryMock.Setup(c => c.GetByIdWithItemsAsync(1)).Returns(Task.FromResult(testedNews));

            

            await newsService.DeleteNewsItem(1);

            _newsRepositoryMock.Verify(c => c.DeleteAsync(It.Is<NewsItem>(arg => arg.Header == "Header" && arg.Id == 1)));


        }
        [Fact]
        public async Task DeleteNewsItemWrongIdTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await newsService.DeleteNewsItem(-1));
        }
        [Fact]
        public async Task GetNewsChartWrongIdTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await newsService.GetNewsChart(-1));
        }
        [Fact]
        public async Task GetNewsChartTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);

            var testedNews = new NewsItem { Header = "Header", Id = 1, ChartItems = new List<ChartItem>
            {
                new ChartItem() { Id = 1, Likes = 20}
            }};

            _newsRepositoryMock.Setup(c => c.GetByIdWithItemsAsync(1)).Returns(Task.FromResult(testedNews));

            var chart = await newsService.GetNewsChart(1);

            Assert.Equal(1, chart.ElementAt(0).Id);
            Assert.Equal(20, chart.ElementAt(0).Likes);
        }
        [Fact]
        public async Task LikeWrongIdTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await newsService.Like(-1, new Employee()));
        }

        [Fact]
        public async Task LikeAlreadyLikedTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            var sender = new Employee { Id = 1 };
            var testedNews = new NewsItem {
                Header = "Header",
                Id = 1,
                Employees = new List<Employee>
                {
                    sender,
                }
                };
            _newsRepositoryMock.Setup(c => c.GetByIdWithItemsAsync(1)).Returns(Task.FromResult(testedNews));
            await Assert.ThrowsAsync<AlreadyLikedException>(async () => await newsService.Like(1, sender));
        }
        [Fact]
        public async Task LikeTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            var sender = new Employee { Id = 1 };
            var testedNews = new NewsItem
            {
                Header = "Header",
                Id = 1,
                TotalLikes = 0,
                Employees = new List<Employee>(),
                ChartItems = new List<ChartItem>()
            };
            _newsRepositoryMock.Setup(c => c.GetByIdWithItemsAsync(1)).Returns(Task.FromResult(testedNews));
            await newsService.Like(1, sender);
            _newsRepositoryMock.Verify(c => c.UpdateAsync(It.Is<NewsItem>(arg => arg.TotalLikes == 1)));
        }
        [Fact]
        public async Task UpdateNewsItemWrongIdTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await newsService.UpdateNewsItem(-1, null,null,null,null));
        }
        [Fact]
        public async Task UpdateNewsItemTest()
        {
            Arrange();
            var newsService = new NewsService(_newsRepositoryMock.Object, _repositoryChart.Object);
            var testedNews = new NewsItem
            {
                Header = "1",
                Author = "1",
                PhotoURL = "1",
                Body = "1",
                Id = 1,
                TotalLikes = 0,
                Employees = new List<Employee>(),
                ChartItems = new List<ChartItem>()
            };
            _newsRepositoryMock.Setup(c => c.GetByIdAsync(1)).Returns(Task.FromResult(testedNews));
            await newsService.UpdateNewsItem(1, "header", "body", "author", "photo");
            _newsRepositoryMock.Verify(c => c.UpdateAsync(It.Is<NewsItem>(arg => arg.Header == "header" 
            && arg.Body == "body" && arg.Author == "author" && arg.PhotoURL == "photo")));
        }


    }

}