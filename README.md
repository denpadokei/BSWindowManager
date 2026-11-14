# BSWindowManager

[![Build and Release](https://github.com/denpadokei/BSWindowManager/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/BSWindowManager/actions/workflows/build-and-release.yml)

シーンが切り替わるたびに Beat Saber のウィンドウを最前面へ引き出す Windows 専用 MOD です。VR プレイ中にデスクトップへ戻ったときなどに、ゲーム画面を前面へ戻せます。

## 機能

- シーン切り替え時に `Beat Saber` というタイトルのウィンドウを検索し、アクティブ化する
- ウィンドウが最小化されている場合は復元してから前面へ移動する
- 前面化が拒否されないよう `ForegroundLockTimeout` を一時的に 0 にし、`AttachThreadInput` で入力スレッドを関連付けてから `SetForegroundWindow` を実行する
- 処理後は `ForegroundLockTimeout` を元の値へ戻す
- 自動アクティブ化の ON/OFF を設定メニューから切り替えられる
- `user32.dll` の API を使うため Windows でのみ動作する

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.40.8 |
| 依存 MOD | BSIPA ^4.3.6 / BeatSaberMarkupLanguage ^1.12.5 / SiraUtil ^3.2.1 |

## インストール

1. Beat Saber を 1.40.8 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/BSWindowManager/releases) から `BSWindowManager-1.0.0-bs1.40.8.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

インストール後は設定で有効なままゲームを起動するだけです。シーンが切り替わるたびに Beat Saber のウィンドウが前面へ移動します。ウィンドウ操作はゲーム内のメニューではなく、ホスト側の Windows に対して行われます。

## 設定

ゲーム内の `設定` から `BS WINDOW MANAGER` を開きます。

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| AutoActive | bool | true | シーン切り替え時に Beat Saber のウィンドウを自動でアクティブにする |

設定は `UserData/BSWindowManager.json` に保存されます。

## ビルド

```powershell
git clone https://github.com/denpadokei/BSWindowManager.git
cd BSWindowManager
dotnet build .\BSWindowManager\BSWindowManager.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
