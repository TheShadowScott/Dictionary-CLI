namespace DictionaryAPI;
public static class EntryBuilder
{

    public static List<LexicalEntry> BuildLexicalEntries(Root root)
    {
        var entries = new List<LexicalEntry>();

        foreach (var result in root.results)
        {
            foreach (var lexicalEntry in result.lexicalEntries)
            {
                entries.Add(lexicalEntry);
            }
        }
        return entries;
    }
    public static List<Entry> BuildEntries(Root root)
    {
        var entries = new List<Entry>();

        foreach (var result in root.results)
        {
            foreach (var lexicalEntry in result.lexicalEntries)
            {
                foreach (var entry in lexicalEntry.entries)
                {
                    entries.Add(entry);
                }
            }
        }
        return entries;
    }

    public static string BuildStringFromSenseEntries(List<Sense> senses)
    {
        var sb = new System.Text.StringBuilder();
        int i = 1;

        foreach (var sense in senses)
        {
            foreach (var definition in sense.definitions)
            {
                sb.AppendLine($"{i++}. {definition}");
            }
        }

        return sb.ToString();
    }

    public static string BuildStringFromEntryList(List<Entry> entries)
    {
        var sb = new System.Text.StringBuilder();
        int i = 1;
        foreach (var entry in entries)
        {
            foreach (var sense in entry.senses)
            {
                foreach (var definition in sense.definitions)
                {
                    sb.AppendLine($"{i++}. {definition}");
                }
            }
        }

        return sb.ToString();
    }

    public static string BuildStringFromLexicalEntryList(List<LexicalEntry> entries)
    {
        var sb = new System.Text.StringBuilder();
        int i = 1;
        foreach (var entry in entries)
        {
            foreach (var sense in entry.entries)
            {
                foreach (var definition in sense.senses)
                {
                    foreach (var def in definition.definitions)
                    {
                        sb.AppendLine($"{i++}. ({MapToPartOfSpeech(entry.lexicalCategory.id)}) {def}");
                    }
                }
            }
        }

        return sb.ToString();
    }


    private static string MapToPartOfSpeech(string input)
    {
        return input switch
        {
            "noun" => "n.",
            "verb" => "v.",
            "adjective" => "adj.",
            "adverb" => "adv.",
            "pronoun" => "pron.",
            "preposition" => "prep.",
            "conjunction" => "conj.",
            "determiner" => "det.",
            "exclamation" => "excl.",
            "interjection" => "interj.",
            _ => input
        };
    }
}