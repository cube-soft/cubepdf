# CubePDF

Copyright (c) 2010 CubeSoft, Inc.  
GNU Affero General Public License version 3 (AGPLv3)  
support@cube-soft.jp  
http://www.cube-soft.jp/cubepdf/

## はじめに

CubePDF は、お使いの様々なアプリケーションから、いつでも必要な時にすばやく
PDF 形式などの文書を作成する事のできる、とても便利なソフトです。
印刷と同じ操作でサッと作成する事ができるので、操作に戸惑うこともありません。
詳細な利用方法については、同梱した「CubePDF ユーザーズマニュアル.pdf」を参照下さい。

CubePDF を使用するためには、Microsoft .NET Framework 3.5 以上がインストールされている
必要があります。Microsoft .NET Framework 3.5 は、以下の URL からダウンロードして下さい。  
http://www.microsoft.com/ja-jp/download/details.aspx?id=22

## 問題が発生した場合

CubePDF は、以下のファイルに実行ログを出力しています。
問題が発生した時は、以下のファイルを添付して
サポート ( support@cube-soft.jp ) までご連絡お願いいたします。

* C:\Program Files\CubePDF\install.log (*1)
* C:\ProgramData\CubeSoft\CubePdf\Log\CubePdf.log
* C:\ProgramData\CubeSoft\CubePdf\Log\CubeProxy.log

(*1) インストール時に指定したフォルダの中にあります。

## 利用ライブラリ

CubePDF は、以下のライブラリを利用しています。
それぞれのライブラリについては、記載した URL から取得することができます。

* GPL Ghostscript
    - GNU Affero General Public License
    - http://www.ghostscript.com/
* iTextSharp
    - GNU Affero General Public License
    - http://sourceforge.net/projects/itextsharp/
    - https://www.nuget.org/packages/iTextSharp/
* log4net
    - Apache License, Version 2.0
    - http://logging.apache.org/log4net/
    - https://www.nuget.org/packages/log4net/
* AlphaFS
    - MIT License
    - http://alphafs.alphaleonis.com/
    - https://www.nuget.org/packages/AlphaFS/

## バージョン履歴

* 2017/05/15 version 1.0.0RC11
    - リモートデスクトップに関わる不都合を修正
    - メイン画面の言語設定に関わる処理を修正
* 2017/02/20 version 1.0.0RC10
    - 特定の条件下で CubePDF メイン画面が表示されない不都合を修正
* 2017/01/27 version 1.0.0RC9
    - Windows ストアアプリから変換できない不都合を修正
    - セキュリティ画面において、何らかの操作を制限している状況で閲覧専用のパスワードを省略した場合、エラーメッセージを表示するように修正
    - メイン画面を日本語および英語に対応
    - メイン画面のレイアウトを修正
    - ログ出力方法を変更
* 2015/09/24 version 1.0.0RC8
    - Windows ストアアプリから変換できない不都合に暫定的に対応
    - ユーザ名に非英数字文字が使用されているアカウントで画像形式に変換しようとするとフリーズする不都合を修正
    - Web 表示用に最適化オプションが有効になっている時に A4 サイズに強制される不都合を修正
    - 設定を保存を実行後にキャンセルボタンを押すとエラーが発生する不都合を修正
    - PDF バージョンを 1.4 以下に設定できない不都合を修正
* 2014/05/12 version 1.0.0RC7
    - 変換可能なファイルの種類から SVG を削除
    - ダウンサンプリングに関連する項目を修正
    - ページの向きの項目を追加
    - 暗号化方式を PDF バージョンによって変更するように修正
    - 結合方法をファイルサイズが小さくなるように修正
    - ファイル作成時に一時領域で処理を行うように修正
    - ポストプロセスの実行方法等を修正
    - 出力ファイル名の初期値の決定方法を一部修正
    - バージョン情報画面に使用環境のシステム（Windows、および .NET Framework）のバージョンを表示するように修正
    - メイン画面の見た目を微調整
    - CubePDF プリンタの用紙サイズのリストを修正
* 2013/08/13 version 1.0.0RC6
    - デスクトップに保存時、手動で「最新の情報に更新」されるまで保存したファイルが表示されない不都合を修正
* 2013/07/09 version 1.0.0RC5
    - 使用しているライブラリ (Ghostscript) のライセンス変更に伴い、CubePDF のライセンスを GNU General Public License (GPLv3) から GNU Affero General Public License (AGPL) に変更
    - CubePDF を使用して変換した PDF ファイルを Adobe Reader 等の一部の PDF 閲覧ソフトで表示した際、テキストの選択状態が不自然になる不都合を修正
    - 出力先ファイルが別プロセスで使用中の場合にダイアログを表示するように修正
    - CubePDF 起動時に電子署名の検証をスキップするように修正
    - ファイル名として使用できない文字列を出力ファイル欄に入力できないように修正
    - アップデートの確認プログラムを実行するタイミングを修正
* 2012/09/21 version 1.0.0RC4
    - 「設定を保存」ボタンを追加
    - 64bit 版 Windows XP でインストールに失敗する不都合を修正
    - アンインストール時にスタートアップに登録されている CubePDF アップデートチェックプログラムが削除されない不都合を修正
    - プリンタスプーラのロックが解除されるタイミングをオプションで選択できるように修正
    - 「出力ファイル」欄の情報をレジストリに保存する際の方法を変更
* 2012/05/25 version 1.0.0RC3
    - CubePDF メイン画面が表示された時点でプリンタスプーラのロックが解除されるように修正
    - ファイル名に日本語以外の文字が含まれている場合、CubePDF メイン画面の「出力ファイル」欄に表示されるファイル名が文字化けする不都合を修正
    - 変換中に CubePDF メイン画面の変換ボタンが押下できる不都合を修正
    - ユーザープログラムを指定するテキストボックスが、ポストプロセスで「ユーザープログラム」を選択しても選択可能な状態にならない不都合を修正
    - 画面の DPI 設定がデフォルトと異なる場合に CubePDF メイン画面のレイアウトが崩れる不都合を修正
    - ポストプロセスで「開く」を選択した場合、関連付けが行われていなければ実行しないように修正
    - インストール直後の「CubePDF ユーザーズマニュアルを表示する」の項目を PDF ファイルに対して関連付けが行われている場合のみ表示するように修正
* 2012/03/28 version 1.0.0RC2
    - cubepdf-redirect.exe の機能を cubepdf.exe に統合
    - 「セキュリティ」タブのレイアウトを変更
    - 「出力ファイル」のテキストボックスに直接ファイル名を入力した際に、自動的に拡張子を補完するように修正
    - パスワードで保護されているPDFファイルに結合する場合、「セキュリティ」タブで入力したパスワードと同じであれば結合できるように修正
    - PDF ファイルの結合に失敗した際に、元の PDF ファイルが破損する不都合を修正
* 2012/01/31 version 1.0.0RC1
    - プリンタポートモニタを修正
    - PDFに埋め込まれている画像をJpeg圧縮するかどうかを選択できるように修正
    - 出力ファイル欄にファイル名として使用できない文字が設定される不都合を修正
* 2011/10/18 version 0.9.9.5β
    - レジストリに定義外の値が指定された場合にエラーが発生する不都合を修正
    - 特定の条件下で「セキュリティ」の項目と「Web 表示用に最適化」の項目を両方とも設定可能になる不都合を修正
    - ツールチップが同時に複数表示される不都合を修正
* 2011/09/20 version 0.9.9.4β
    - ファイル名に特定の文字列が含まれる場合に変換に失敗する不都合を修正
    - 特定の条件下でインストール時に発生する不都合を修正
* 2011/07/01 version 0.9.9.3β
    - インストール時に Microsoft .NetFramework がインストールされていない環境での不都合を修正
    - TIFF 形式の場合に解像度が選択できない不都合を修正
* 2011/05/17 version 0.9.9.2β
    - ポストプロセスの設定が保存されない不都合を修正
    - Microsoft .NetFramework 2.0 のみがインストールされている環境での不都合を修正
* 2011/04/29 version 0.9.9.1β
    - デスクトップフォルダがリネームされている場合の不都合を修正
    - Microsoft .NetFramework 4 のみがインストールされている環境での不都合を修正
* 2011/04/27 version 0.9.9β
    - インストール時に環境によって発生していた不都合を修正
    - 縦書き問題への対応など生成されるファイルの精度を改善
    - 同名のファイルが存在している場合の処理に「リネーム」を追加
    - ファイル名にドットが含まれていた場合にも末尾に拡張子が自動的に付加されるように修正
    - 出力先を選択する時の「ファイル保存ダイアログ」からもファイルタイプを変更できるように修正
    - 出力先の初期設定を「マイドキュメント」から「デスクトップ」に変更
    - パスワードの打ち間違え時に確認ダイアログが赤く表示されるように変更
* 2010/12/16 version 0.9.3.1β
    - パスワードを設定した場合に正常に PDF ファイルが生成されない問題を修正
* 2010/12/13 version 0.9.3β
    - 複数のユーザがログオンしている時に発生する問題を修正
    - 生成された PDF ファイルを各種 PDF ビューア上でコピー&ペーストすると文字化けする問題を修正
    - 生成される PDF ファイルに一部フォーマット不正が存在した問題を修正
    - バージョンダイアログを表示するように変更
    - ghostscript を 8.71 にバージョンダウン
* 2010/11/08 version 0.9.2.3β
    - ポストプロセスの実行に失敗する問題を修正
    - アップデートチェック・プログラムの起動に関する問題を修正
* 2010/11/01 version 0.9.2.2β
    - 設定保存されてある出力先ディレクトリが存在しない場合、初期設定の出力先ディレクトリを指定するように修正
    - アンインストール時に CubePDF 関連の各種プロセスが実行されているかどうかをチェックするように修正
* 2010/10/21 version 0.9.2.1β
    - ユーザ名とユーザプロファイルが異なる場合の問題を修正 ( http://blog.cube-soft.jp/?p=87 )
    - アンインストール時にアップデートチェックプログラムが削除されない問題を修正 ( http://blog.cube-soft.jp/?p=124 )
* 2010/09/27 version 0.9.2β
    - 10 ページ以上のファイルを画像ファイルに変換する際に、ファイル名の連番がずれる問題を修正
    - リモートホストからログインした場合に、ローカルマシン上にウィンドウが表示される問題を修正
    - ダウンサンプリングオプションを「サブサンプル」に指定した場合にエラーが発生する問題を修正
    - 一般ユーザで実行するとエラーになる問題を修正
    - .NetFramework 4 のみがインストールされている環境で起動しない問題を修正
    - タブの外観を調整
    - 設定の保存機能を追加
* 2010/07/23 version 0.9.1β
    - 文書プロパティに特定の文字を入力すると Adobe reader などで表示できない問題を修正
    - 文書プロパティを設定しない場合に、ファイル名などが日本語だと PDF ファイルのプロパティが文字化けする問題を修正
    - CubePDF の起動時にウィンドウが最前面に表示されない問題を修正
    - ユーザ名とユーザプロファイルのディレクトリ名が異なる場合に不都合が発生する問題を修正
    - 変換処理中にプログレスバーを表示するように変更
    - ポストプロセスで「ユーザープログラム」を選択できるように変更（アドバンスモードの場合のみ）
    - 同名のファイルが存在する場合に「上書き」だけではなく「先頭に結合」、「末尾に結合」する機能を追加
* 2010/07/07 version 0.9.0β
    - 最初の公開バージョン
