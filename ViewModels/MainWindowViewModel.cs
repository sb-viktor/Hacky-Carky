using CommunityToolkit.Mvvm.ComponentModel;
using Hacky_Carky.Models;

namespace Hacky_Carky.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _textBoxText;

    [ObservableProperty]
    private string? _transliteratedText;

    partial void OnTextBoxTextChanged(string? value)
        => TransliteratedText = CzechTransliterator.Transliterate(value ?? string.Empty);
}
