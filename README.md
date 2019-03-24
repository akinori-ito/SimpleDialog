# SimpleDialog

Microsoft Cognitive Service の音声認識機能とSAPI 5.0の音声合成機能を使った簡単な音声対話アプリです。学習用です。

## 使う前に
[Microsoft Cognitive Service](https://azure.microsoft.com/ja-jp/services/cognitive-services/)への登録が必要です。
[Microsoft Azure](https://azure.microsoft.com)のアカウントがない人は、まずそれを作りましょう。ちょっとだけなら無料です。

次にCognitive Service のSpeechをサービスに追加して、キーを取得します。キーが2つできますがどちらでもいいようです。
そのキーを、Dialog.cs の "YOUR-MICROSOFT-COGNITIVESERVICE-SPEECH-KEY" の部分に入れます。

## ビルド
Visual Studio 2017 を使います。まず参照にSystem.Speechを追加し、さらにNuget Package Manager で Microsoft.CognitiveService.Speech とNMeCabを入れるとビルドできるはずです。
