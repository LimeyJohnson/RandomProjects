<?php

if (@$_GET['dl']!='')
{
    if (!file_exists('download.zip'))
    {
	set_time_limit(600);
	$zip = new ZipArchive;
	$res = $zip->open('download.zip', ZipArchive::CREATE);
	if ($res === TRUE)
	{
	    foreach (scandir('./') as $key => $fileName)
	    {
		if (substr($fileName,0,1)=='.' || $fileName==basename(__FILE__))
		    continue;
    
		if (substr($fileName, 0,12)=='download.zip')
		{
		    unlink('./'.$fileName);
		    continue;
		}		
		
		$zip->addFile('./'.$fileName, $fileName);	   
	    }
	    $zip->close();	
	}
    }
    header("Location: http://" . $_SERVER['HTTP_HOST']  . dirname($_SERVER['SCRIPT_NAME']) . '/download.zip');
    die();
}


if (file_exists('./playlist.json'))
{
    header("Content-Type: application/json");
    
    if (@$_GET['callback']!='')
    {
      echo $_GET['callback'].'(';
    }
    
    readfile('./playlist.json');
    
    if (@$_GET['callback']!='')
    {
      echo ');';
    }       
    die();
}

// load getID-lib - alter path at will
require_once('../getid3/getid3.php');
$getID3 = new getID3;

$playlist = array();

foreach (scandir('./') as $key => $fileName)
{
    if (substr($fileName,0,1)=='.' || $fileName==basename(__FILE__))
	continue;
    
    if (getimagesize($fileName)!==false)
	continue;
    
    $ThisFileInfo = $getID3->analyze($fileName);
    
    $playlist[ substr($fileName, 0, strrpos($fileName, '.')) ][] =  $ThisFileInfo;
}

$result = array();

$count=0;
foreach ($playlist as $baseName => $fileInfos)
{
    $count++;

    foreach($fileInfos as $key => $fileInfo)
    {
	
	// try to find a tracknumber
	if ($_searchTrackNumber = in_multi_array('track_number', $fileInfo))
	{
	    $trackNumber = $_searchTrackNumber[0];    
	}
	elseif ($_searchTrackNumber = in_multi_array('tracknumber', $fileInfo))
	{
	    $trackNumber = $_searchTrackNumber[0];    
	}
	else
	{
	    $trackNumber = $count;    
	}
	
	
	// get title
	if (!@in_array('title', @$result[$trackNumber]))
	{
	    if ($_searchTitle = in_multi_array('title', $fileInfo))
	    {
		$result[$trackNumber]['config']['title'] = $_searchTitle[0];
	    }	    
	}
	
	if ($_searchFilename = in_multi_array('filename', $fileInfo))
	{
	    $_searchFormat = in_multi_array('fileformat', $fileInfo);
	    
	    $result[$trackNumber][$key] = array(
		'src'	=> "http://" . $_SERVER['HTTP_HOST']  . dirname($_SERVER['SCRIPT_NAME']) . '/' . $_searchFilename,
		'type'	=> 'audio/' . $_searchFormat
	    );
	}	   	

	if ($_searchFilename = in_multi_array('filename', $fileInfo))
	{
	    if (!empty($fileInfo['comments']['picture'][0]))
	    {
		$picture_array = $fileInfo['comments']['picture'][0];
		
		$coverArtFileName =  substr($_searchFilename, 0, strrpos($_searchFilename, '.')) . '.' . str_replace('image/', '', $picture_array['image_mime']);
		if (!file_exists($coverArtFileName))
		{		
		    file_put_contents($coverArtFileName, $picture_array['data']);  
		}
		$result[$trackNumber]['config']['poster'] = "http://" . $_SERVER['HTTP_HOST']  . dirname($_SERVER['SCRIPT_NAME']) . '/' . $coverArtFileName;		
	    }
	 
	}
	array_multisort($result[$trackNumber], SORT_DESC);

    }
}

// write cached playlist
$fh = fopen("playlist.json", 'w') or die("can't open file");
fwrite($fh, json_encode($result));
fclose($fh);


// send playlist
header("Content-Type: application/json");

if (@$_GET['callback']!='')
{
  echo $_GET['callback'].'(';
}

echo json_encode($result);

if (@$_GET['callback']!='')
{
  echo ');';
}   
    



function in_multi_array($needle, $haystack) {

    if(@$haystack[$needle]) {    
       return $haystack[$needle];
    }
    else
    {
	foreach($haystack as $key => $val)
	{
	    if(is_array($val))
	    {
		if($result = in_multi_array($needle, $val))
		{
		    return $result;                   
		}
	    }
	}
   }
   return false;
} 
