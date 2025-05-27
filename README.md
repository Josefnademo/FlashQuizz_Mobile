# FlashQuizz_Mobile
Une application mobile de création, gestion et utilisation de cartes d'apprentissages (flashcards) avec MAUI.

## 📁 FlashQuizz (project root)
## 📁 Views — UI pages (XAML screens)
- MainPage.xaml — Main Menu screen with:
   - "Start Learning" button → navigates to the learning session
   - "My Cards" button → navigates to card management

- MesCartesPage.xaml — view and manage all flashcards
   - Implements CollectionView for card list display
   - Each card shows question preview
   - Swipe actions for edit/delete

- AddEditCardPage.xaml — add or edit a flashcard
   - Form with question and answer fields
   - Character count display (0/255)
   - Cancel button → returns to previous page using Shell navigation
   - Save button → persists changes and returns to card list

- LearningPage.xaml — the learning session interface (question/answer flow)
   - Card flip animation for question/answer
   - Shake detection for "don't know" responses
   - Progress tracking during session

- SessionSummaryPage.xaml — shows summary after a learning session
   - Displays session statistics
   - Purple background (#CCC5EB) with result logo
   - Shows time spent and mastery metrics

## 📁 ViewModels — Page logic
- MainViewModel.cs — logic for card overview and actions
   - Implements navigation commands (ViewCards, Cancel)
   - Handles basic CRUD operations
   - Example of Cancel implementation:
     ```csharp
     [RelayCommand]
     public async Task Cancel()
     {
         try
         {
             await Shell.Current.GoToAsync($"///{nameof(MyCardsPage)}");
         }
         catch (Exception ex)
         {
             // Fallback navigation
             await Shell.Current.GoToAsync("..");
         }
     }
     ```

- MyCardsViewModel.cs — logic for adding/editing a card
   - Inherits from MainViewModel
   - Implements IRecipient for card updates
   - Manages card selection and editing

- LearningViewModel.cs — logic for the learning process
   - Manages card flipping animation
   - Tracks session progress
   - Handles shake detection responses

- SessionSummaryViewModel.cs — logic for displaying results
   - Calculates final statistics
   - Processes session data for display
  
- CardsViewModelBase.cs — Base class for card-related ViewModels
   - Provides common card management functionality
   - Implements shared properties and commands

## 📁 Converters — 
- InverseBoolConverter.cs — Inverts boolean values for UI binding
   - Used for showing/hiding card faces
   - Simplifies XAML bindings

## 📁 Models — Data structures
- Card.cs — model representing a flashcard
   - Properties: Id, Question, Answer
   - Validation logic for content

- SessionStats.cs — stores learning session data
   - Tracks time spent
   - Records correct/incorrect answers
   - Calculates mastery percentages

## 📁 Services — App services
- CardService.cs — manages data (CRUD) for flashcards
   - Implements database operations
   - Handles card persistence
   - Provides async data access

- FlashCardDbContext.cs — SQLite database context
   - Defines database schema
   - Manages entity relationships

- **not implemented** LearningService.cs (optional) — manages learning logic

## 📁 Resources
- Styles.xaml — defines shared button styles, margins, font styles, etc.
   - Implements consistent UI appearance
   - Defines reusable button styles
   - Example:
     ```xaml
     <Style x:Key="SecondaryButtonStyle" TargetType="Button">
         <!-- Button styling properties -->
     </Style>
     ```

- Colors.xaml — defines color resources
   - Contains app color scheme
   - Used for consistent theming
   - Example: PrimaryTextColor, CardBackground

## Key Implementation Details
1. Navigation:
   - Uses Shell navigation for consistent page flow
   - Implements both relative (".." ) and absolute ("///Page") navigation
   - Handles navigation errors with fallback options

2. Data Binding:
   - Uses MVVM pattern with ObservableProperty attributes
   - Implements INotifyPropertyChanged for UI updates
   - Uses Command binding for user actions

3. Error Handling:
   - Implements try-catch blocks for critical operations
   - Provides user feedback through DisplayAlert
   - Includes debug logging for troubleshooting
