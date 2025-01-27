using GameStore.Frontend.Models;
namespace GameStore.Frontend.Clients;

public class GamesClient
{
    private readonly List<GameSummary> games = 
    [
        new() {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
        new() {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new() {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 9, 27)
        }
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetNames() => [.. games];
    public void AddGame(GameDetails game) {
        ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);

        var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));
        var gameSummary = new GameSummary() {
            Id = games.Count +1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
        this.games.Add(gameSummary);
    }

    private Genre GetGenreById(int id) {
        return genres.Single(genre => genre.Id == id);
    }
    private GameSummary GetGameSummaryById(int id) {
        GameSummary? game = games.Find(game => game.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    public void UpdateGame(GameDetails updatedGame) {
        ArgumentException.ThrowIfNullOrWhiteSpace(updatedGame.GenreId);

       var genre = GetGenreById(int.Parse(updatedGame.GenreId));
       var existingGame = GetGameSummaryById(updatedGame.Id);
       existingGame.Name = updatedGame.Name;
       existingGame.Genre = genre.Name;
       existingGame.Price = updatedGame.Price;
       existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public GameDetails GetGame(int id){
        GameSummary? game = games.Find(game => game.Id == id);
        ArgumentNullException.ThrowIfNull(game);

        Genre genre = genres.Single(genre => string.Equals(
            genre.Name, 
            game.Genre, 
            StringComparison.OrdinalIgnoreCase));

        return new GameDetails() {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void DeleteGame(int id) {
        var existingGame = GetGameSummaryById(id);
        if(existingGame is not null) {
            games.Remove(existingGame);
        }
    }
}
