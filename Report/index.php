<?PHP header("Content-Type: text/html; charset=utf-8"); 
            ?>
<script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
<DOCTYPE html>
<html>
    <head>
            <title>Документация на методы курсового проекта</title>
            <link rel="stylesheet" href="style.css">
    </head>
        <body>
            <h1>Документация на методы курсового проекта</h1>
            <br>
            <a href ="#Resistance">Метод уровня сопротивления</a>
            <a href ="#Support">Метод уровня поддержки</a>
            <a href ="#SMA">Метод SMA</a>
            <?php 
            $host = "localhost";
            $Base = "c913670j_report";
            $password = "123456";
            $User = "c913670j_report";
            $connect =mysql_connect($host, $Base,$password);
             mysql_select_db($User,$connect);
            include "Bd.php";//подключаем файл с настройками к бд
             $id = $_GET["id"];
             $buy = $_GET["buy"];
             $sell = $_GET["sell"];
             $data = $_GET["data"];
             $Login = $_GET["Login"];//создать различные исключения  по id и выборку
             $result = mysql_query("INSERT INTO `EURUSD` (`id`, `buy`, `sell`, `data`, `Login`) VALUES ('$id', '$buy', '$sell', '$data', '$Login');");
             $myrow =  mysql_fetch_array($result);
            for($i = 0; $i<3;$i++ )
            {
                switch($i)
                {
                    case 0: ?>  <a name ="Resistance"></a> <?php $amethod ="Resistance"; $title = "Уровень сопротивления";$method = "1"; $date = "1.01.16";break;
                    case 1: ?>  <a name ="Support"></a> <?php $amethod ="Support"; $title = "Уровень поддержки";$method = "1"; $date = "1.01.16";break;
                    case 2: ?>  <a name ="SMA"></a> <?php $amethod ="SMA"; $title = "SMA";$method = "1"; $date = "1.01.16";break;
                }
                 $class = "artical";
                 include "Hats/title.php";
            }
            ?>
            <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br><br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br> <br>  
    <?php                  
    include "Hats/footer.php";
    ?>
        </body>
</html>

