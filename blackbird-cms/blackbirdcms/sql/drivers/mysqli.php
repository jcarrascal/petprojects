<?php

class DriverMySQLi
{
	private $mInstance;

	/**
	 * Connect to the specified database server.
	 * @param array $dsn
	 */
	function connect($dsn)
	{
		$this->mInstance = @new MySQLi($dsn['host'], $dsn['user'], isset($dsn['pass']) ? $dsn['pass'] : null, substr($dsn['path'], 1));
		if ($this->mInstance->connect_errno)
			throw new BB\SQL\SqlException($this->mInstance->connect_error, $this->mInstance->connect_errno);
	}

	/**
	 * Disconnect from the server. You should not call any other method until you call connect() again.
	 */
	function disconnect()
	{
		$this->mInstance->close();
	}

	/**
	 * Properly quote the given string to make it safe to concatenate with other SQL sentences.
	 * @param string $str
	 * @return string
	 */
	function quote($str)
	{
		return "'" . $this->mInstance->real_escape_string($str) . "'";
	}

	/**
	 * Execute the given query. Returns MySQLi_Result object for select statement,
	 * TRUE for insert, update and delete statements, FALSE on error.
	 * @param string $query
	 * @return mixed 
	 */
	function query($query)
	{
		$result = $this->mInstance->query($query);
		if ($result === false)
			throw new BB\SQL\SqlException($this->mInstance->error, $this->mInstance->errno);
		return $result;
	}

	/**
	 * Retrieve the current row in the specified result as an object.
	 * @param MySQLi_Result $result
	 * @return object
	 */
	function fetchObject($result)
	{
		return $result->fetch_object();
	}

	/**
	 * Retrieve the current row in the specified result as an indexed array.
	 * @param MySQLi_Result $result
	 * @return <type>
	 */
	function fetchRow($result)
	{
		return $result->fetch_row();
	}

	/**
	 * Insert the given values in the $tableName table and returns the generated id.
	 * @param string $tableName
	 * @param array $columnNames
	 * @param array $quotedValues
	 * @return int Or FALSE in case of an error.
	 */
	function insert($tableName, $columnNames, $quotedValues)
	{
		$sql = "insert into $tableName (" . implode(', ', $columnNames) . ")\n    values ("
			. implode(', ', $quotedValues) . ")";
		if ($this->mInstance->query($sql) === false)
			return false;
		return $this->mInstance->insert_id;
	}
}

