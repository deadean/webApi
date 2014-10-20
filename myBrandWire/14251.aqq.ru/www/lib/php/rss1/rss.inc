<?php
/*   CRss ������ 1.0 �� 02.01.2007
*    ����� ��� �������� ������� �������� RSS
*    http://www.caseclub.ru
*    ����������� ��� �����������
*/
class CRss
{
 var $Title;          // ��������� ������
 var $Link;           // ������ �� ������� ��������
 var $Copyright;      // ��������
 var $Description;    // �������� ������
 var $LastBuildDate;  // ���� ���������� ��������� (�� ��������� �������)
 var $Language;        // ����
 var $PubDate;        // ���� ����������
 var $ManagingEditor;  // E-mail ���������
 var $WebMaster;      // E-mail webmaster
 var $Category;       // ���������

 var $Query;          // ���������� �������
 var $Connect;           // ��� ���������� � ����� ������
 var $Result;         // ��� �������� ����������

 function Translate($text)    // �������� ��� ������
 {


    $trans = array("<" => "&lt;", ">" => "&gt;",'"' => "&quot;","&" => "&amp;");
    $text=strtr($text,$trans);
    $array=explode("<br>",$text);
    $count=count($array);
    return $text;

 }

 function Query()
 {
   $this->Result = mysql_query($this->Query) or die("Query failed");
 }
 function Open($Server,$DataBase,$Login,$Password)    // ������� MySql
  {
  $this->Connect = mysql_connect($Server, $Login, $Password ) or die("Could not connect");
          mysql_select_db($DataBase) or die("Could not select database");

  }
  function Close()  // ������� MySql
  {
     mysql_free_result($this->Result);
     mysql_close($this->Connect);
  }


 function PrintHeader()   // ������ ���������
 {
      header("Content-Type: application/xml ");   // ����� �������, ��� ��� ������ XML
       $RN="\r\n";
      $End="?";
      $Date=date("r");   // ���� � ������� Mon, 25 Dec 2006 10:23:37 +0400
      print "<$End";
      print "xml version=\"1.0\" encoding=\"UTF-8\" $End> $RN";
      print "<rss version=\"2.0\">$RN";
      print "   <channel>$RN";
      print "       <title>$this->Title</title>$RN";
      print "       <category>$this->Category</category>$RN";
      print "       <link>$this->Link</link>$RN";
      print "       <copyright>$this->Copyright</copyright>$RN";
      print "       <description>$this->Description</description>$RN";
      print "       <lastBuildDate>$this->LastBuildDate</lastBuildDate>$RN";
      print "       <language>$this->Language</language>$RN";
      print "       <pubDate>$this->PubDate</pubDate>$RN";
      print "       <docs>http://blogs.law.harvard.edu/tech/rss</docs>$RN";
      print "       <managingEditor>$this->ManagingEditor</managingEditor>$RN";
      print "       <webMaster>$this->WebMaster</webMaster>$RN";
}
 function PrintBody($Title,$Link,$Description,$Category,$PubDate)   // ������ ����
{
       $RN="\r\n";
      //$Title =$this->Translate($Title);
      //$Link =$this->Translate($Link);
      $Description =$this->Translate($Description);
      print "              <item>$RN";
      print "                <title>$Title</title>$RN";
      print "                 <link>$Link</link>$RN";
      print "                 <description>$Description</description>$RN";
      print "                 <category>$Category</category>$RN";
      print "                 <pubDate>$PubDate</pubDate>$RN";
      print "                 <guid>$Link</guid>$RN";
      print "              </item>$RN";
}
 function PrintFooter()   // ������ ���������
 {
    print "   </channel>$RN";
    print "</rss>$RN";
 }


}
?>
