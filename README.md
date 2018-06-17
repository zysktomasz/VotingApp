
Aplikacja do głosowania, tworzenia ankiet, sprawdzania ich statystyk(*todo*) oraz generowania raportów(*todo*). Korzystanie z usług wymaga rejestracji, zaimplementowanej z wykorzystaniem ASP.NET Core Identity.

<img src="https://i.imgur.com/EciRM02.png"/>

----------


### Wykorzystane technologie i rozwiązania
- ASP .NET Core 2.0 (wzorzec projektowy MVC)
- Razor Views + Bootstrap (prosty front aplikacji)
- npm (manager pakietów jquery,bootstrap)
- Entity Framework Core (Code First, obsługa baz danych MSSQL)
- ASP .NET Core Identity (autoryzacja/autentykacja)
- **`DbInitializer.cs`** do utworzenia startowych kont i ankiet
- **`LoginStatusViewComponent`** do prezentacji belki logowania/zarządzania ankietami dla zalogowanych, zgodnie z [tym artykułem](https://andrewlock.net/an-introduction-to-viewcomponents-a-login-status-view-component/)
- wykorzystanie nuget pakietu **`OdeToCode.AddFeatureFolders`** od K. Scott Allena (opisane [tutaj](https://odetocode.com/blogs/scott/archive/2016/11/29/addfeaturefolders-and-usenodemodules-on-nuget-for-asp-net-core.aspx)) do organizacji controllerów i views wewnątrz nowego folderu *Features*


----------

----------


### Struktura solucji projektu
Struktura reprezentuje moją pierwszą próbę zastosowania się do ***Clean Architecture*** bazując na wskazówkach opisanych w [Architecting Modern Web Apps with ASP.NET Core 2 and Azure](https://docs.microsoft.com/en-us/dotnet/standard/modern-web-apps-azure-architecture/common-web-application-architectures)

- `VotingApp.Core` zawiera klasy POCO modeli (Answer, Poll, Vote, ApplicationUser) oraz interfejsy (IPoll, IVote)
- `VotingApp.Infrastructure` zawiera DbContext i migracje z EF Core oraz implementacje interfejsów (`/Services`)
- `VotingApp.Web` - warstwa prezentacyjna z views i controllers pogrupowanymi w dodatkowym folderze *`Features`*

![struktura projektu](https://i.imgur.com/KSbSEYu.png)



----------

----------


### Dodawanie ankiety
Każdy zalogowany użytkownik może dodać ankietę. Pola formularza: pytanie, odpowiedzi, status (*public/private*).

> **Private** - ankieta niewidoczna na liście ankiet, dostępna jedynie z adresu 

![Dodawanie ankiety](https://i.imgur.com/7xQGoW7.png)


Przycisk *"Add new answer field"* dodaje, z użyciem javascriptu, kolejne pole na odpowiedź. *"Remove"* zaś usuwa powiązane do siebie pole. Poniżej kod zawarty w `AddPoll.cshtml`

```js
    $(document).ready(function () {
        $('.answers_wrapper').on("click", ".remove_button", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').parent('div').remove();
        })

        var answerHTML = '<div class="input-group col-8 mb-3"><input class="form-control" type="text" data-val="true" data-val-required="The Answers field is required." id="Answers" name="Answers" /><div class="input-group-append"> <button class="btn btn-outline-secondary remove_button" type="button">Remove</button></div></div>';
        $(".add_button").click(function () {
            $(".answers_wrapper").append(answerHTML);
        });

    });
```

W ViewModelu `PollAddViewModel.cs` został również dopisany customowy atrybut, zapewniający że lista odpowiedzi w formularzu nie będzie pusta oraz nie będzie zawierała pustych elementów.

```csharp
public class PollAddViewModel
    {
        [Required]
        public string Question { get; set; }
        [EnsureICollectionElementNotNull(ErrorMessage = "Answer cannot be empty.")]
        public ICollection<string> Answers { get; set; }
        public PollStatus Status { get; set; }
    }

    public class EnsureICollectionElementNotNullAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as ICollection<string>;
            if (list == null) return false;

            if (list.Any(answer => answer == null))
            {
                return false;
            }
            return true;
        }
    }
```

Poniżej metoda HttpPost`AddPoll()` w controllerze odpowiadająca za dodawanie ankiety.

```csharp

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPoll([Bind("Question,Answers,Status")] PollAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                Poll newPoll = new Poll
                {
                    Question = model.Question,
                    Answers = model.Answers.Select(s => new Answer
                    {
                        Description = s,
                    }).ToList(),
                    Status = model.Status,
                    User = await _userManager.GetUserAsync(User)
                };

                _polls.Add(newPoll);
                return RedirectToAction(nameof(MyPollsItem), new { pollId = newPoll.PollId });
            }

            return View(model);
        }
```

----------

----------

### Głosowanie
Głosowanie wymaga bycia zalogowanym, głos można oddać raz, ostatecznie (bez możliwości zmiany).

![głosowanie na ankietę](https://i.imgur.com/XIPfLgd.png)

##### Wyniki ankiety
Widok po zagłosowaniu, wyświetla ilość głosów oraz Pie Chart z Google Charts

![Wyniki ankiety](https://i.imgur.com/E8iwCcI.png)

###### Metoda zwracająca dane json z wynikami ankiety

```csharp
[AllowAnonymous]
        [HttpGet]
        [Route("[controller]/[action]/{pollId}")]
        public JsonResult ResultsPieChart(int pollId)
        {
            var answers = _polls.GetPollById(pollId).Answers;

            var resultsData = answers.Select(a => new {
                    description = a.Description,
                    votes = a.Votes
                }).ToList();
            return Json(resultsData);
        }
```

----------

----------

### Zarządzanie ankietami
##### Lista własnych ankiet, z przyciskiem usuwającym

![Lista ankiet](https://i.imgur.com/9XGb5FY.png)

##### Panel zarządzający ankiety
![panel ankiety](https://i.imgur.com/zcqmOqy.png)

>*"Get summary in .PDF"* - TODO

