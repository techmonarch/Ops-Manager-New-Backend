using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Identity.Users;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Notifications.Commands;
public class CreateNotificationCommand : IRequest<ApiResponse<BatchResponse>>
{
    public string Title { get; set; } = default!;
    public string Topic { get; set; } = default!;
    public string Body { get; set; } = default!;
    public Dictionary<string, string> Data { get; set; } = new();
    public IFormFile? Image { get; set; }
}

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, ApiResponse<BatchResponse>>
{
    private readonly IUserService _userService;
    private readonly IFileStorageService _fileStorageService;

    public CreateNotificationCommandHandler(IUserService userService, IFileStorageService fileStorageService)
    {
        _userService = userService;
        _fileStorageService = fileStorageService;
    }

    public async Task<ApiResponse<BatchResponse>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var fcmTokens = await _userService.GetAllFcmTokensAsync();
        string imageUrl = await _fileStorageService.UploadAsync<string>(request.Image, FileType.Image, cancellationToken);
        request.Data.Add("image", imageUrl);
        var message = new MulticastMessage()
        {
            Tokens = fcmTokens,
            Notification = new Notification
            {
                Title = request.Title,
                Body = request.Body,
                ImageUrl = imageUrl
            },
            Data = request.Data
        };

        var response = await FirebaseMessaging.DefaultInstance.SendEachForMulticastAsync(message, cancellationToken);
        Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
        return new ApiResponse<BatchResponse>(true, "Notifications Sent Successfully", response);
    }
}
