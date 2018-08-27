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

        #region Initialization
        /// <summary>
        /// Retrieves alphabet to display
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAlphabet")]
        public HttpResponseMessage GetAlphabet()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = _gameService.getAlphabet() });
        }

        /// <summary>
        /// Reset Game and retrieves reseted state of game
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ResetGame")]
        public HttpResponseMessage ResetGame()
        {
            GameState gameState = _gameService.resetGame();
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = gameState });
        }
        #endregion


        #region GameMoves

        /// <summary>
        /// Process a game move and retrieves state of the game
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ProcessLetter")]
        public HttpResponseMessage ProcessLetter(string pWord, string pLetter)
        {
            string word = (pWord == null) ? "" : pWord;
            GameState gameState = _gameService.processLetter(word.ToUpper(), pLetter.ToUpper());
            return Request.CreateResponse(HttpStatusCode.OK, new { success = true, data = gameState });
        }
        #endregion
    }
}
