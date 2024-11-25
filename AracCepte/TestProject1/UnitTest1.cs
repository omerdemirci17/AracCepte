using Xunit; // xUnit framework
using Moq; // Mocklama için
using Microsoft.AspNetCore.Mvc; // Controller iþlemleri için
using Microsoft.EntityFrameworkCore; // DbContext iþlemleri için
using AracCepte.WebUI.Areas.Users.Controllers;
using AracCepte.WebUI.Areas.Users.Models;
using AracCepte.Entity.Entities;
using AracCepte.DataAccess.Context;
using System.Collections.Generic;
using System.Linq;
using AracCepte.Business.Abstract;// IGenericService için
using AracCepte.DTO.DTOs.UserDtos;
using AutoMapper;// DTO ile Entity dönüþümleri için
using OnlineEdu.API.Controllers;
using AracCepte.DTO.DTOs.VehicleDtos;
using AracCepte.API.Controllers; // DTO sýnýflarý için


namespace TestProject1
{
	public class ContactControllerTests
	{
		[Fact]
		public void Contact_Get_ReturnsViewWithModel()
		{
			// Arrange
			var controller = new ContactController();

			// Act
			var result = controller.Contact();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsType<ContactFormModel>(viewResult.Model);
		}

		[Fact]
		public void Contact_Post_WithValidModel_RedirectsToSuccess()
		{
			// Arrange
			var controller = new ContactController();
			var validModel = new ContactFormModel
			{
				// Model validasyonlarýný karþýlayan örnek veriler girin
				Name = "Zehra",
				Email = "zehra@example.com",
				//Message = "Test mesajý"
			};

			// Act
			var result = controller.Contact(validModel);

			// Assert
			var redirectResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("Success", redirectResult.ActionName);
		}

		[Fact]
		public void Contact_Post_WithInvalidModel_ReturnsView()
		{
			// Arrange
			var controller = new ContactController();
			var invalidModel = new ContactFormModel(); // Eksik veri ile model

			controller.ModelState.AddModelError("Name", "Name is required");

			// Act
			var result = controller.Contact(invalidModel);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal("Contact", viewResult.ViewName);
		}
	}
	public class UserRegisterControllerTests
	{
		[Fact]
		public void Register_ReturnsRegisterView()
		{
			// Arrange
			var controller = new UserRegisterController();

			// Act
			var result = controller.Register();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal("Register", viewResult.ViewName);
		}
	}

	public class UserLoginControllerTests
	{
		private readonly Mock<DbSet<User>> _mockSet;
		private readonly Mock<AracCepteContext> _mockContext;
		private readonly List<User> _users;

		public UserLoginControllerTests()
		{
			// Kullanýcý listesi
			_users = new List<User>
			{
				new User { Email = "zehra@example.com", Password = "password123" },
				new User { Email = "test@example.com", Password = "testpass" }
			};

			// DbSet'i mocklama
			var queryableUsers = _users.AsQueryable();
			_mockSet = new Mock<DbSet<User>>();
			_mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUsers.Provider);
			_mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryableUsers.Expression);
			_mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUsers.ElementType);
			_mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUsers.GetEnumerator());

			// Context'i mocklama
			_mockContext = new Mock<AracCepteContext>();
			_mockContext.Setup(c => c.Users).Returns(_mockSet.Object);
		}

		[Fact]
		public void Login_Get_ReturnsViewWithModel()
		{
			// Arrange
			var controller = new UserLoginController(_mockContext.Object);

			// Act
			var result = controller.Login();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.IsType<LoginFromModel>(viewResult.Model);
		}

		[Fact]
		public void Login_Post_WithValidCredentials_RedirectsToHomePage()
		{
			// Arrange
			var controller = new UserLoginController(_mockContext.Object);
			var validModel = new LoginFromModel
			{
				Email = "zehra@example.com",
				Password = "password123"
			};

			// Act
			var result = controller.Login(validModel);

			// Assert
			var redirectResult = Assert.IsType<RedirectToActionResult>(result);
			Assert.Equal("HomePage", redirectResult.ActionName);
			Assert.Equal("UserHome", redirectResult.ControllerName);
		}

		[Fact]
		public void Login_Post_WithInvalidCredentials_ReturnsViewWithError()
		{
			// Arrange
			var controller = new UserLoginController(_mockContext.Object);
			var invalidModel = new LoginFromModel
			{
				Email = "invalid@example.com",
				Password = "wrongpassword"
			};

			// Act
			var result = controller.Login(invalidModel);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.False(controller.ModelState.IsValid);
			Assert.True(controller.ModelState.ContainsKey(""));
		}

		[Fact]
		public void Login_Post_WithInvalidModelState_ReturnsView()
		{
			// Arrange
			var controller = new UserLoginController(_mockContext.Object);
			var model = new LoginFromModel(); // Eksik verilerle

			controller.ModelState.AddModelError("Email", "Email is required");

			// Act
			var result = controller.Login(model);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
		}
	}
	public class UserHomeControllerTests
	{
		[Fact]
		public void HomePage_ReturnsHomePageView()
		{
			// Arrange
			var controller = new UserHomeController();

			// Act
			var result = controller.HomePage();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result); // ViewResult döndüðünü doðrula
			Assert.Equal("HomePage", viewResult.ViewName); // View adýnýn "HomePage" olduðunu doðrula
		}
	}
	public class UsersControllerTests
	{
		private readonly Mock<IGenericService<User>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly UsersController _controller;

		public UsersControllerTests()
		{
			_mockService = new Mock<IGenericService<User>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new UsersController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllUsers()
		{
			// Arrange
			var userList = new List<User> { new User(), new User() };
			_mockService.Setup(service => service.TGetList()).Returns(userList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(userList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsUser()
		{
			// Arrange
			var user = new User { Id = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(user);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(user, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("kullanici Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createUserDto = new CreateUserDto { Name = "Zehra" };
			var user = new User { Name = "Zehra" };
			_mockMapper.Setup(mapper => mapper.Map<User>(createUserDto)).Returns(user);

			// Act
			var result = _controller.Create(createUserDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni kullanici Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<User>(u => u.Name == "Zehra")), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateUserDto = new UpdateUserDto { Id = 1, Name = "UpdatedName" };
			var user = new User { Id = 1, Name = "UpdatedName" };
			_mockMapper.Setup(mapper => mapper.Map<User>(updateUserDto)).Returns(user);

			// Act
			var result = _controller.Update(updateUserDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("kullanici Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<User>(u => u.Name == "UpdatedName")), Times.Once);
		}
	}
	public class VehiclesControllerTests
	{
		private readonly Mock<IGenericService<Vehicle>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly VehiclesController _controller;

		public VehiclesControllerTests()
		{
			_mockService = new Mock<IGenericService<Vehicle>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new VehiclesController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllVehicles()
		{
			// Arrange
			var vehicleList = new List<Vehicle> { new Vehicle(), new Vehicle() };
			_mockService.Setup(service => service.TGetList()).Returns(vehicleList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(vehicleList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsVehicle()
		{
			// Arrange
			var vehicle = new Vehicle { ID = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(vehicle);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(vehicle, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("arac Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createVehicleDto = new CreateVehicleDto { Brand = "Car1" };
			var vehicle = new Vehicle { Brand = "Car1" };
			_mockMapper.Setup(mapper => mapper.Map<Vehicle>(createVehicleDto)).Returns(vehicle);

			// Act
			var result = _controller.Create(createVehicleDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni arac Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Vehicle>(v => v.Brand == "Car1")), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateVehicleDto = new UpdateVehicleDto { ID= 1, Brand = "UpdatedCar" };
			var vehicle = new Vehicle { ID = 1, Brand = "UpdatedCar" };
			_mockMapper.Setup(mapper => mapper.Map<Vehicle>(updateVehicleDto)).Returns(vehicle);

			// Act
			var result = _controller.Update(updateVehicleDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("arac Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Vehicle>(v => v.Brand == "UpdatedCar")), Times.Once);
		}
	}

	public class SocialMediasControllerTests
	{
		private readonly Mock<IGenericService<SocialMedia>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly SocialMediasController _controller;

		public SocialMediasControllerTests()
		{
			_mockService = new Mock<IGenericService<SocialMedia>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new SocialMediasController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllSocialMedias()
		{
			// Arrange
			var socialMediaList = new List<SocialMedia> { new SocialMedia(), new SocialMedia() };
			_mockService.Setup(service => service.TGetList()).Returns(socialMediaList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(socialMediaList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsSocialMedia()
		{
			// Arrange
			var socialMedia = new SocialMedia { SocialMediaId= 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(socialMedia);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(socialMedia, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Sosyal Medya Alaný Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createSocialMediaDto = new AracCepte.DTO.DTOs.SocialMediaDtos.CreateSocialMediaDto { Title = "Instagram" };
			var socialMedia = new SocialMedia { Title= "Instagram" };
			_mockMapper.Setup(mapper => mapper.Map<SocialMedia>(createSocialMediaDto)).Returns(socialMedia);

			// Act
			var result = _controller.Create(createSocialMediaDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni Sosyal Medya Alaný Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<SocialMedia>(sm => sm.Title == "Instagram")), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateSocialMediaDto = new AracCepte.DTO.DTOs.SocialMediaDtos.UpdateSocialMediaDto { SocialMediaId= 1, Title = "Updated Instagram" };
			var socialMedia = new SocialMedia { SocialMediaId= 1,Title = "Updated Instagram" };
			_mockMapper.Setup(mapper => mapper.Map<SocialMedia>(updateSocialMediaDto)).Returns(socialMedia);

			// Act
			var result = _controller.Update(updateSocialMediaDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Sosyal Medya Alaný Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<SocialMedia>(sm => sm.Title  == "Updated Instagram")), Times.Once);
		}
	}
	public class ReservationsControllerTests
	{
		private readonly Mock<IGenericService<Reservation>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly ReservationsController _controller;

		public ReservationsControllerTests()
		{
			_mockService = new Mock<IGenericService<Reservation>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new ReservationsController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllReservations()
		{
			// Arrange
			var reservationList = new List<Reservation> { new Reservation(), new Reservation() };
			_mockService.Setup(service => service.TGetList()).Returns(reservationList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(reservationList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsReservation()
		{
			// Arrange
			var reservation = new Reservation { Id = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(reservation);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(reservation, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("rezervasyon Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createReservationDto = new AracCepte.DTO.DTOs.ReservationDtos.CreateReservationDto { VehiclesID = 1, RenterID= 1 /*,ReservationStartDate = "01-12-2024", ReservationEndDate = "2024-12-10" */};
			var reservation = new Reservation {		VehiclesID = 1, RenterID = 1 /*,ReservationStartDate = "01-12-2024", ReservationEndDate = "2024-12-10" */ };
			_mockMapper.Setup(mapper => mapper.Map<Reservation>(createReservationDto)).Returns(reservation);

			// Act
			var result = _controller.Create(createReservationDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni rezervasyon Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Reservation>(r => r.VehiclesID == 1)), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateReservationDto = new AracCepte.DTO.DTOs.ReservationDtos.UpdateReservationDto{ Id = 1, VehiclesID = 2, RenterID = 1 /*,ReservationStartDate = "01-12-2024", ReservationEndDate = "2024-12-10" */ };
			var reservation = new Reservation { Id = 1, VehiclesID = 2,RenterID = 1 /*,ReservationStartDate = "01-12-2024", ReservationEndDate = "2024-12-10" */};
			_mockMapper.Setup(mapper => mapper.Map<Reservation>(updateReservationDto)).Returns(reservation);

			// Act
			var result = _controller.Update(updateReservationDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("rezervasyon Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Reservation>(r => r.Id == 1)), Times.Once);
		}
	}
	public class RatingsControllerTests
	{
		private readonly Mock<IGenericService<Rating>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly RatingsController _controller;

		public RatingsControllerTests()
		{
			_mockService = new Mock<IGenericService<Rating>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new RatingsController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllRatings()
		{
			// Arrange
			var ratingList = new List<Rating> { new Rating(), new Rating() };
			_mockService.Setup(service => service.TGetList()).Returns(ratingList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(ratingList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsRating()
		{
			// Arrange
			var rating = new Rating { Id = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(rating);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(rating, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("puanlama Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createRatingDto = new AracCepte.DTO.DTOs.RatingDtos.CreateRatingDto { Point = 5, Comment = "Excellent" };
			var rating = new Rating {Point= 5, Comment = "Excellent" };
			_mockMapper.Setup(mapper => mapper.Map<Rating>(createRatingDto)).Returns(rating);

			// Act
			var result = _controller.Create(createRatingDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni puanlama Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Rating>(r => r.Point == 5)), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateRatingDto = new AracCepte.DTO.DTOs.RatingDtos.UpdateRatingDto{ Id = 1, Point = 4, Comment = "Good" };
			var rating = new Rating { Id = 1, Point = 4, Comment = "Good" };
			_mockMapper.Setup(mapper => mapper.Map<Rating>(updateRatingDto)).Returns(rating);

			// Act
			var result = _controller.Update(updateRatingDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("puanlama Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Rating>(r => r.Id == 1)), Times.Once);
		}
	}
	public class PaymentsControllerTests
	{
		private readonly Mock<IGenericService<Payment>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly PaymentsController _controller;

		public PaymentsControllerTests()
		{
			_mockService = new Mock<IGenericService<Payment>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new PaymentsController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllPayments()
		{
			// Arrange
			var paymentList = new List<Payment> { new Payment(), new Payment() };
			_mockService.Setup(service => service.TGetList()).Returns(paymentList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(paymentList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsPayment()
		{
			// Arrange
			var payment = new Payment { Id = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(payment);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(payment, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("odeme  Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createPaymentDto = new AracCepte.DTO.DTOs.PaymentDtos.CreatePaymentDto{PaymentAmount = 100};
			var payment = new Payment { PaymentAmount = 100 };
			_mockMapper.Setup(mapper => mapper.Map<Payment>(createPaymentDto)).Returns(payment);

			// Act
			var result = _controller.Create(createPaymentDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni odeme  Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Payment>(p => p.PaymentAmount == 100)), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updatePaymentDto = new AracCepte.DTO.DTOs.PaymentDtos.UpdatePaymentDto{ Id = 1, PaymentAmount = 150,};
			var payment = new Payment { Id = 1, PaymentAmount= 150 };
			_mockMapper.Setup(mapper => mapper.Map<Payment>(updatePaymentDto)).Returns(payment);

			// Act
			var result = _controller.Update(updatePaymentDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("odeme Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Payment>(p => p.Id == 1)), Times.Once);
		}
	}
	public class InsurancesControllerTests
	{
		private readonly Mock<IGenericService<Insurance>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly InsurancesController _controller;

		public InsurancesControllerTests()
		{
			_mockService = new Mock<IGenericService<Insurance>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new InsurancesController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllInsurances()
		{
			// Arrange
			var insuranceList = new List<Insurance> { new Insurance(), new Insurance() };
			_mockService.Setup(service => service.TGetList()).Returns(insuranceList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(insuranceList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsInsurance()
		{
			// Arrange
			var insurance = new Insurance { Id = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(insurance);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(insurance, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("sigorta Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createInsuranceDto = new AracCepte.DTO.DTOs.InsuranceDtos.CreateInsuranceDto { VehiclesId=1};
			var insurance = new Insurance {VehiclesId = 1 };
			_mockMapper.Setup(mapper => mapper.Map<Insurance>(createInsuranceDto)).Returns(insurance);

			// Act
			var result = _controller.Create(createInsuranceDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni sigorta  Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Insurance>(i => i.VehiclesId == 1)), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateInsuranceDto = new AracCepte.DTO.DTOs.InsuranceDtos.UpdateInsuranceDto { Id = 1, VehiclesId= 1 };
			var insurance = new Insurance { Id = 1};
			_mockMapper.Setup(mapper => mapper.Map<Insurance>(updateInsuranceDto)).Returns(insurance);

			// Act
			var result = _controller.Update(updateInsuranceDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Sigorta Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Insurance>(i => i.Id == 1 && i.VehiclesId== 1)), Times.Once);
		}
	}
	public class BannersControllerTests
	{
		private readonly Mock<IGenericService<Banner>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly BannersController _controller;

		public BannersControllerTests()
		{
			_mockService = new Mock<IGenericService<Banner>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new BannersController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllBanners()
		{
			// Arrange
			var bannerList = new List<Banner> { new Banner(), new Banner() };
			_mockService.Setup(service => service.TGetList()).Returns(bannerList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(bannerList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsBanner()
		{
			// Arrange
			var banner = new Banner {BannerID = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(banner);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(banner, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Banner Alaný Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createBannerDto = new AracCepte.DTO.DTOs.BannerDtos.CreateBannerDto { Title = "New Banner" };
			var banner = new Banner { Title = "New Banner" };
			_mockMapper.Setup(mapper => mapper.Map<Banner>(createBannerDto)).Returns(banner);

			// Act
			var result = _controller.Create(createBannerDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni Banner Alaný Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<Banner>(b => b.Title == "New Banner")), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateBannerDto = new AracCepte.DTO.DTOs.BannerDtos.UpdateBannerDto { BannerID= 1,Title = "Updated Banner" };
			var banner = new Banner { BannerID= 1, Title= "Updated Banner" };
			_mockMapper.Setup(mapper => mapper.Map<Banner>(updateBannerDto)).Returns(banner);

			// Act
			var result = _controller.Update(updateBannerDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Banner Alaný Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<Banner>(b => b.BannerID== 1 && b.Title == "Updated Banner")), Times.Once);
		}
	}
	public class AboutsControllerTests
	{
		private readonly Mock<IGenericService<About>> _mockService;
		private readonly Mock<IMapper> _mockMapper;
		private readonly AboutsController _controller;

		public AboutsControllerTests()
		{
			_mockService = new Mock<IGenericService<About>>();
			_mockMapper = new Mock<IMapper>();
			_controller = new AboutsController(_mockService.Object, _mockMapper.Object);
		}

		[Fact]
		public void Get_ReturnsAllAbouts()
		{
			// Arrange
			var aboutList = new List<About> { new About(), new About() };
			_mockService.Setup(service => service.TGetList()).Returns(aboutList);

			// Act
			var result = _controller.Get();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(aboutList, okResult.Value);
		}

		[Fact]
		public void GetById_ReturnsAbout()
		{
			// Arrange
			var about = new About { AboutId = 1 };
			_mockService.Setup(service => service.TGetById(1)).Returns(about);

			// Act
			var result = _controller.GetById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(about, okResult.Value);
		}

		[Fact]
		public void DeleteById_ReturnsSuccessMessage()
		{
			// Arrange
			_mockService.Setup(service => service.TDelete(1));

			// Act
			var result = _controller.DeleteById(1);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Hakkýmýzda Alaný Silindi", okResult.Value);
		}

		[Fact]
		public void Create_ReturnsSuccessMessage()
		{
			// Arrange
			var createAboutDto = new AracCepte.DTO.DTOs.AboutDtos.CreateAboutDto { Description = "About content" };
			var about = new About { Description = "About content" };
			_mockMapper.Setup(mapper => mapper.Map<About>(createAboutDto)).Returns(about);

			// Act
			var result = _controller.Create(createAboutDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Yeni Hakkýmýzda Alaný Oluþturuldu", okResult.Value);
			_mockService.Verify(service => service.TCreate(It.Is<About>(a => a.Description == "About content")), Times.Once);
		}

		[Fact]
		public void Update_ReturnsSuccessMessage()
		{
			// Arrange
			var updateAboutDto = new AracCepte.DTO.DTOs.AboutDtos.UpdateAboutDto { AboutId = 1, Description = "Updated content" };
			var about = new About { AboutId = 1, Description = "Updated content" };
			_mockMapper.Setup(mapper => mapper.Map<About>(updateAboutDto)).Returns(about);

			// Act
			var result = _controller.Update(updateAboutDto);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Hakkýmda Alaný Güncellendi", okResult.Value);
			_mockService.Verify(service => service.TUpdate(It.Is<About>(a => a.AboutId == 1 && a.Description == "Updated content")), Times.Once);
		}
	}
}
