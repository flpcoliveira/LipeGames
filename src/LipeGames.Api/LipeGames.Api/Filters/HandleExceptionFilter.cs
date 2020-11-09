using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Excecoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Threading.Tasks;

namespace LipeGames.Api.Filters
{
    public class HandleExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;
            var response = context.HttpContext.Response;

            response.ContentType = "application/json";

            var mensagem = "Ocorreu um erro interno. Favor entrar em contato com o nosso suporte";           

            if (exception is EntidadeNaoEncotradaException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                mensagem = exception.Message;
            } else if (exception is AutenticacaoExcecao)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                mensagem = exception.Message;
            } 

            var erroAplicacaoDto = new ErroAplicacaoDto
            {
                Mensagem = mensagem
            };
            context.Result = new JsonResult(erroAplicacaoDto);

            return Task.CompletedTask;
        }
    }
}
