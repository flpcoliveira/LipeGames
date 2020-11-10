using LipeGames.Dominio.Dto;
using LipeGames.Dominio.Excecoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
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

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var resumo = "Ocorreu um erro interno. Favor entrar em contato com o nosso suporte";

            var mensagensValidacao = new Dictionary<string, string>();

            if (exception is EntidadeNaoEncotradaException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                resumo = exception.Message;
            } else if (exception is AutenticacaoExcecao)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                resumo = exception.Message;
            } else if (exception is RegraNegocioExcecao)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                resumo = exception.Message;
                mensagensValidacao = ((RegraNegocioExcecao)exception).Mensagens;
            }

            var erroAplicacaoDto = new ErroAplicacaoDto
            {
                Resumo = resumo,
                Erros = mensagensValidacao
            };
            context.Result = new JsonResult(erroAplicacaoDto);

            return Task.CompletedTask;
        }
    }
}
