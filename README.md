# Telegram highlighter bot

## Описание

Бот для Телеграм, который получает код, а возвращает ссылку на тот же код, где подсвечен синтаксис, чтобы можно было удобно поделиться с друзьями.

1) Добавление различных языков программирования
2) Добавление разных форматов кода (из файла, из текста, распарсить код по ссылке на pastebin, gist.github и т.д)

ID бота в Телеграме: @oophighlighterbot

## План рассказа
* [Application/Highlighter.cs#L10](https://github.com/Phil9l/telegram-code-highlighter/blob/master/CodeHighlighter/CodeHighlighter/Application/Highlighter.cs#L10) — класс, который позволяет получить из строки и наследника `BaseTokenizer` набор токенов или отформатированный в HTML код.
* [Domain/BaseTokenizer.cs#L6](https://github.com/Phil9l/telegram-code-highlighter/blob/master/CodeHighlighter/CodeHighlighter/Domain/BaseTokenizer.cs#L6) — абстрактный класс, наследники которого используются для токенизации текста. Необходимо отнаследоваться от него и реализовать абстрактные методы, чтобы написать токенизатор какого-то языка программирования.
* [Domain/Tokenizers/PyTokenizer.cs#L5](https://github.com/Phil9l/telegram-code-highlighter/blob/master/CodeHighlighter/CodeHighlighter/Domain/Tokenizers/PyTokenizer.cs#L5) — пример токенизатора для работы с Python-кодом.
* [UI/Bot.cs#L16](https://github.com/Phil9l/telegram-code-highlighter/blob/master/CodeHighlighter/CodeHighlighter/UI/Bot.cs#L16) — класс, реализующий всё общение с API Телеграма.
* [Tests](https://github.com/Phil9l/telegram-code-highlighter/tree/master/CodeHighlighter/CodeHighlighter/Tests) — тесты.

### Точки расширения
1. Возможность наследоваться от BaseTokenizer, таким образом добавлять работу с новыми языками программирования.
2. Возможность получать код из Telegram разными способами:
  * Из файла
  * Plain-text в сообщении
  * Один из стандартных сайтов для передачи кода (pastebin.com, gist.github.com и т.д.)
