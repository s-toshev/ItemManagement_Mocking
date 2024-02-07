using NUnit.Framework;
using Moq;
using ItemManagementApp.Services;
using ItemManagementLib.Repositories;
using ItemManagementLib.Models;
using System.Collections.Generic;
using System.Linq;

namespace ItemManagement.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
     
        private ItemService _itemService;

        private Mock<IItemRepository> _mockItemRepository;
        

        [SetUp]
        public void Setup()
        {
            // Arrange: Create a mock instance of IItemRepository
            _mockItemRepository = new Mock<IItemRepository>();
            // Instantiate ItemService with the mocked repository
            _itemService = new ItemService(_mockItemRepository.Object);
        }

        [Test]
        public void AddItem_ShouldCallAddItemOnRepository()
        {
            //Arrange
            var item = new Item {Name = "Test item" };
            _mockItemRepository.Setup(x => x.AddItem(item));
            //Act
            _itemService.AddItem(item.Name);

            //Assert
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()));

        }

        [Test]
        public void AddItem_ShouldThrowErrorIfNameIsInvalid()
        {
            //Arrange
            string invalidName = "";

            _mockItemRepository.Setup(x => x.AddItem(It.IsAny<Item>())).Throws<ArgumentException>();
            //Act&Assert
            Assert.Throws<ArgumentException>(() => _itemService.AddItem(invalidName));
            _mockItemRepository.Verify(x => x.AddItem(It.IsAny<Item>()));

        }


        [Test]
        public void GetAllItems_ShouldReturnAllItems()
        {
            //Arrange

            var items = new List<Item>() { new Item {Id = 1, Name = "SampleItem" } };
            _mockItemRepository.Setup(x => x.GetAllItems()).Returns(items);
            //Act
            var result = _itemService.GetAllItems();

            //Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
            _mockItemRepository.Verify(x => x.GetAllItems(), Times.Once()); 

        }

        [Test]
        public void GetItemByIdShouldReturnItemByID_IfItemExists()
        {
            //Arrange
            var item = new Item { Id = 1, Name = "Single item" };
            _mockItemRepository.Setup(x => x.GetItemById(item.Id)).Returns(item);

            //Act
            var result = _itemService.GetItemById(item.Id);

            //Assert
            Assert.NotNull(result);
            Assert.That(result.Name, Is.EqualTo(item.Name));
            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());  
        }

        [Test]
        public void GetItemByIdShouldReturnNull_IfItemDoesNotExists()
        {
            //Arrange
            Item item = null;
            _mockItemRepository.Setup(x => x.GetItemById(It.IsAny<int>())).Returns(item);

            //Act
            var result = _itemService.GetItemById(124);

            //Assert
            Assert.Null(result);
            _mockItemRepository.Verify(x => x.GetItemById(It.IsAny<int>()), Times.Once());
        }


        [Test]
        public void UpdateItem_ShouldCallUpdateItemOnRepository()
        {
            //Arrange
            var item = new Item { Name = "Sample item", Id = 1 };
            _mockItemRepository.Setup(x => x.GetItemById(item.Id)).Returns(item);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));
            //Act&Assert
            _itemService.UpdateItem(item.Id, "Sample item Updated");

            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Once());


        }

        [Test]
        public void UpdateItem_ShouldNotUpdateItemIfItemDoesNotExist()
        {
            //Arrange
            var nonExistingId = 1;
            _mockItemRepository.Setup(x => x.GetItemById(nonExistingId)).Returns<Item>(null);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>()));
            //Act
            _itemService.UpdateItem(nonExistingId, "Doesnotmattr");

            //Assert
            _mockItemRepository.Verify(x => x.GetItemById(nonExistingId), Times.Once());    
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Never());


        }

        [Test]
        public void UpdateItem_ShouldThrowExceptionIfItemNameIsInvalid()
        {
            //Arrange
            var item = new Item { Name = "Sample item", Id = 1 };
            _mockItemRepository.Setup(x => x.GetItemById(item.Id)).Returns(item);
            _mockItemRepository.Setup(x => x.UpdateItem(It.IsAny<Item>())).Throws<ArgumentException>();
           
            //Act&Assert
            Assert.Throws<ArgumentException>(() => _itemService.UpdateItem(item.Id, ""));
           
            _mockItemRepository.Verify(x => x.GetItemById(item.Id), Times.Once());
            _mockItemRepository.Verify(x => x.UpdateItem(It.IsAny<Item>()), Times.Once());


        }


        [Test]
        public void DeleteItem_ShouldCallDeleteItemOnRepository()
        {
            //Arrange
            var itemId = 12;
            _mockItemRepository.Setup(x => x.DeleteItem(itemId));

            //Act
            _itemService.DeleteItem(itemId);

            //Assert
            _mockItemRepository.Verify(x => x.DeleteItem(itemId), Times.Once());    

        }

        [Test]
        public void ValidateItemName_WhenNameIsValid_ShouldReturnTrue()
        {
            //Arrange


            //Act


            //Assert


        }

        [Test]
        public void ValidateItemName_WhenNameIsTooLong_ShouldReturnFalse()
        {
            //Arrange


            //Act


            //Assert


        }

        [Test]
        public void ValidateItemName_WhenNameIsEmpty_ShouldReturnFalse()
        {
            
        }
    }
}