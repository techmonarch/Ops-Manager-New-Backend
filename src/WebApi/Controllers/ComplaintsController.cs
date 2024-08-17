using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Complaints.Commands;
using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Application.Features.Complaints.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class ComplaintsController : VersionNeutralApiController
{
    /// <summary>
    /// Post a new Complaint.
    /// </summary>
    /// <param name="request">The Complaint details.</param>
    /// <returns>The ID of the created Complaint.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Complaints)]
    [OpenApiOperation("Post a new Complaint.", "Creates a new Complaint with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateComplaintCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Post a new Complaint Category.
    /// </summary>
    /// <param name="request">The Complaint Category details.</param>
    /// <returns>The ID of the created Complaint Category.</returns>
    [HttpPost("category")]
    [MustHavePermission(OPSAction.Create, OPSResource.ComplaintCategories)]
    [OpenApiOperation("Post a new Complaint Category.", "Creates a new Complaint Category with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateCategoryAsync(CreateCategoryCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Post a new Complaint Sub-Category.
    /// </summary>
    /// <param name="request">The Complaint Sub-Category details.</param>
    /// <returns>The ID of the created Complaint Sub-Category.</returns>
    [HttpPost("sub-category")]
    [MustHavePermission(OPSAction.Create, OPSResource.ComplaintSubCategories)]
    [OpenApiOperation("Post a new Complaint Sub-Category.", "Creates a new Complaint Sub-Category with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateSubCategoryAsync(CreateSubCategoryCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Get all Complaint .
    /// </summary>
    /// <returns>A paginated list of all complaints details.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Complaints)]
    [OpenApiOperation("Get Complaint.", "Retrieve a paginated list of complaint.")]
    public async Task<ApiResponse<PaginationResponse<ComplaintDetailDto>>> GetComplaints([FromQuery] GetComplaintsQuery request)
    {
        return await Mediator.Send(request);
    }

    /// <summary>
    /// Get Complaint Categories.
    /// </summary>
    /// <returns>A paginated list of all complaint categories details.</returns>
    [HttpGet("categories")]
    [MustHavePermission(OPSAction.View, OPSResource.ComplaintCategories)]
    [OpenApiOperation("Get Complaint Categories.", "Retrieve a paginated list of complaint categories.")]
    public async Task<ApiResponse<PaginationResponse<ComplaintCategoryDetailDto>>> GetComplaintCategories([FromQuery] GetComplaintCategoriesQuery request)
    {
        return await Mediator.Send(request);
    }

    /// <summary>
    /// Get Complaint sub Categories.
    /// </summary>
    /// <returns>A paginated list of all complaint sub categories details.</returns>
    [HttpGet("sub-categories")]
    [MustHavePermission(OPSAction.View, OPSResource.ComplaintSubCategories)]
    [OpenApiOperation("Get Complaint Categories.", "Retrieve a paginated list of complaint sub categories.")]
    public async Task<ApiResponse<PaginationResponse<ComplaintSubCategoryDetailDto>>> GetComplaintSubCategories([FromQuery] GetComplaintSubCategoriesQuery request)
    {
        return await Mediator.Send(request);
    }
}