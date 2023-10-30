using ECommerceBack.Common;
using ECommerceBack.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;

namespace ECommerceBack.WebApi.Filters;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly NotificationContext _notificationContext;

    public NotificationFilter(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.PossuiNotificacoes)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            await context.HttpContext.Response.WriteAsJsonAsync(new RespostaApiDto(_notificationContext.Notificacoes));
            return;
        }

        await next();
    }
}
