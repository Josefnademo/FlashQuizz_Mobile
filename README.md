# FlashQuizz_Mobile
Une application mobile de création, gestion et utilisation de cartes d'apprentissages (flashcards) avec MAUI.

## 📁 FlashQuizz (project root)
## 📁 Views — UI pages (XAML screens)
- MainPage.xaml — Main Menu screen with:

   - "Start Learning" button → navigates to the learning session

   -  "My Cards" button → navigates to card management

- MesCartesPage.xaml — view and manage all flashcards

- AddEditCardPage.xaml — add or edit a flashcard

- LearningPage.xaml — the learning session interface (question/answer flow)

- SessionSummaryPage.xaml — shows summary after a learning session

## 📁 ViewModels — Page logic
- MainMenuViewModel.cs — logic for MainPage (commands for navigation)

- MainViewModel.cs — logic for card overview and actions

- AddEditCardViewModel.cs — logic for adding/editing a card

- LearningViewModel.cs — logic for the learning process

- SessionSummaryViewModel.cs — logic for displaying results

## 📁 Models — Data structures
- Card.cs — model representing a flashcard

-  **not implemented** SessionStats.cs (optional) — stores learning session data (like score, progress, etc.)

## 📁 Services — App services
- CardService.cs — manages data (CRUD) for flashcards

- NavigationService.cs — handles page navigation

-  **not implemented** LearningService.cs (optional) — manages learning logic (question generation, tracking)

## 📁 Resources
- Styles.xaml — defines shared button styles, margins, font styles, etc.

- Colors.xaml — defines color resources (e.g., PrimaryTextColor, PrimaryButtonStyle)
