<?php

namespace BB\SQL;

class SqlException extends \Exception
{
}

class Connection
{
	static function connect($dsn)
	{
		$dsn = is_array($dsn) ? $dsn : parse_url($dsn);
		$filename = LIBRARY_PATH . '/sql/drivers/' . $dsn['scheme'] . '.php';
		if (!file_exists($filename))
			throw new SqlException("Unknown driver file /sql/drivers/{$dsn['scheme']}.php");
		require_once $filename;
		$classname = 'Driver' . $dsn['scheme'];
		if (!class_exists($classname))
			throw new SqlException("Unknown driver class $classname");
		$driver = new $classname($dsn);
		$driver->connect($dsn);
		return new Connection($dsn, $driver);
	}

	private $mDSN;

	private function __construct($dsn, $driver)
	{
		$this->mDSN = $dsn;
		$this->mDriver = $driver;
	}

	function __destruct()
	{
		$this->mDriver->disconnect();
	}

	function fetchAll($sql)
	{
		if (($result = $this->mDriver->query($sql)) !== false)
		{
			$array = array();
			while (($object = $this->mDriver->fetchObject($result)) != null)
				$array[] = $object;
			return $array;
		}
		return false;
	}

	function fetchAsoc($sql)
	{
		if (($result = $this->mDriver->query($sql)) !== false)
		{
			$array = array();
			while (($row = $this->mDriver->fetchRow($result)) != null)
				$array[$row[0]] = $row[1];
			return $array;
		}
		return false;
	}

	function fetchColumn($sql)
	{
		if (($result = $this->mDriver->query($sql)) !== false)
		{
			$array = array();
			while (($row = $this->mDriver->fetchRow($result)) != null)
				$array[] = $row[0];
			return $array;
		}
		return false;
	}

	function fetchScalar($sql)
	{
		if (($result = $this->mDriver->query($sql)) !== false)
		{
			$array = array();
			if (($row = $this->mDriver->fetchRow($result)) != null)
				return $row[0];
		}
		return false;
	}
}
