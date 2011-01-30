<?php


class DriverMySQLi
{
	private $mInstance;

	function connect($dsn)
	{
		$this->mInstance = new MySQLi($dsn['host'], $dsn['user'], $dsn['pass'], substr($dsn['path'], 1));
		$errorCode = mysqli_connect_errno();
		if ($errorCode)
			throw new BB\SQL\Sql\Exception(mysqli_connect_error(), $errorCode);
	}

	function disconnect()
	{
		$this->mInstance->close();
	}

	function quote($str)
	{
		return "'" . $this->mInstance->real_escape_string($str) . "'";
	}

	function query($query)
	{
		return $this->mInstance->query($query);
	}

	function fetchObject($result)
	{
		return $result->fetch_object();
	}

	function fetchRow($result)
	{
		return $result->fetch_row();
	}

	function insert($tableName, $columnNames, $quotedValues)
	{
		$sql = "insert into $tableName (" . implode(', ', $columnNames) . ")\n    values ("
			. implode(', ', $quotedValues) . ")";
		if ($this->mInstance->query($sql) === false)
			return false;
		return $this->mInstance->insert_id;
	}
}

