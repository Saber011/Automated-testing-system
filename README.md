# Automated-testing-system
## Как запустить backend
Убедитесь, что у Вас установлен версия .NET 5

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">dotnet --version</span></code></pre>

В случае ошибки выполнения команды, необходимо установить нужную версию SDK: https://docs.microsoft.com/ru-ru/dotnet/core/install/ 
## Запустите проект через dotnet builder

перейти в папку с проектом (путь может отличаться):

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">cd Automated-testing-system/Automated.Testing.System/Automated.Testing.System.Web/</span></code></pre>

Восстановите все зависимости решения:

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">dotnet restore</span></code></pre>

И запустите проект

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">dotnet run</span></code></pre>


Собрать приложение (необязательно) можно с помощью команды

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">dotnet publish -c Releasen</span></code></pre>

Для запуска тестов
Необходимо перейти в проект Automated.Testing.System.IntegrationTests/
И выполнить команду:
<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">dotnet test</span></code></pre>
## Как запустить frontend
Убедитесь, что у Вас установлены node и npm
<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">node -v</span></code></pre>
<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">npm -v</span></code></pre>
В случае ошибки выполнения команды, необходимо установить https://docs.npmjs.com/downloading-and-installing-node-js-and-npm

перейти в папку с проектом (путь может отличаться):

<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">cd /front/Automated.Testing.System/</span></code></pre>
Выполнить команды:
<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">npm i</span></code></pre>
<pre class="code highlight js-syntax-highlight plaintext white" lang="plaintext"><code><span id="LC1" class="line" lang="plaintext">npm run start:local</span></code></pre>
