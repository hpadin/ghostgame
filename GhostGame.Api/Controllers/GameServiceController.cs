using GhostGame.Services;
using GhostGame.Services.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Unity;

namespace GhostGame.Api.Controllers
{

    [RoutePrefix("api/GameService")]
    public class GameServiceController : ApiController
    {
        IGameService _gameService;

        public GameServiceController(GameService gameService)
        {
            _gameService = gameService;
        }

        #region Alphabet
        /// <summary>
        /// Retrieves alphabet to display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAlphabet")]
        public HttpResponseMessage GetAlphabet()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = "abcdefghijklmnopqrstuvwxyz" });
        }
        #endregion

        #region GameMove

        /// <summary>
        /// Retrieves alphabet to display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetGameState")]
        public HttpResponseMessage GetGameState()
        {
            GameState gameState = _gameService.getCurrentState();
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = gameState });
        }

        /// <summary>
        /// Retrieves alphabet to display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ProcessLetter")]
        public HttpResponseMessage ProcessLetter(string pLetter)
        {
            GameState gameState = _gameService.processLetter(pLetter.ToUpper());
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = gameState });
        }
        #endregion

        /// <summary>
        /// Reset Game
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ResetGame")]
        public HttpResponseMessage ResetGame()
        {
            GameState gameState = _gameService.resetGame();
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = gameState });
        }

    }
}
