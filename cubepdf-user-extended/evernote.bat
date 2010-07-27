@echo off
:: ------------------------------------------------------------------------- ::
::
::  evernote.bat
::
::  Copyright (c) 2010 CubeSoft. All rights reserved.
::
::  :: で始まる行はコメントとして利用しています．
::
::  ''注意''
::  このスクリプトを使用するためには，実行環境に Evernote for Windows
::  がインストールされている必要があります．
::  Evernote for Windows は以下の URL からダウンロードして下さい．
::  http://www.evernote.com/about/download/windows.php
::
::  Last-update: Mon 26 Jul 2010 10:05:00 JST
::
:: ------------------------------------------------------------------------- ::

:: ------------------------------------------------------------------------- ::
::
::  Evernote にアップロードするための設定
::
::  evernote: Evernote のインストールフォルダを指定する
::  username: 省略時は最終ログインユーザ
::  password: username 省略時は使用しない
::
:: ------------------------------------------------------------------------- ::
set evernote="C:\Program Files\Evernote\Evernote3.5\ENScript.exe"
set username=""
set password=""

:: ------------------------------------------------------------------------- ::
::
::  実際の処理
::
::  デフォルトでは実行するたびにサーバと同期している．
::  手動で同期する場合は，syncDatabase の行をコメントアウトする．
::
:: ------------------------------------------------------------------------- ::
if not exist %1 (
    echo %1: ファイルが見つかりませんでした
    exit /b
)

echo %evernote% createNote /s %1
echo %evernote% syncDatabase

if %username% == "" (
    %evernote% createNote /s %1
    %evernote% syncDatabase
) else (
    %evernote% createNote /s %1 /u %username% /p %password%
    %evernote% syncDatabase /u %username% /p %password%
)

exit /b
