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

	function quote($value)
	{
		return $this->mDriver->quote($value);
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

	function fetchRow($sql)
	{
		if (($result = $this->mDriver->query($sql)) !== false)
			return $this->mDriver->fetchObject($result);
		return false;
	}

	function fetchAssoc($sql)
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

	function execute($sql)
	{
		return $this->mDriver->query($sql);
	}

	/**
	 * Insert a new row in the specified table with the values in the
	 * associative array or object $columns and return the generated id if any.
	 * @param string $table
	 * @param mixed $columns Associative array or object.
	 * @return mixed Returns FALSE on error or the generated id if any.
	 */
	function insert($table, $columns)
	{
		if (is_object($columns))
			$columns = \get_object_vars($columns);
		return $this->mDriver->insert($table, \array_keys($columns), $this->quoteValues($columns));
	}

	/**
	 * Update the row specified by $where in the $table table with the values given in $columns.
	 * @param string $table
	 * @param mixed $columns Associative array or object.
	 * @param mixed $where
	 * @return bool Returns FALSE on error.
	 */
	function update($table, $columns, $where)
	{
		if (is_object($where))
			$where = \get_object_vars($where);
		if (is_array($where))
			$where = implode(' and ', $this->assignColumns($where));
		if (is_object($columns))
			$columns = \get_object_vars($columns);
		$sql = "update $table set " . implode(', ', $this->assignColumns($columns)) . "\n    where $where";
		return $this->execute($sql);
	}

	private function quoteValues($columns)
	{
		$values = array();
		foreach ($columns as $value)
			$values[] = !\is_numeric($value) ? $this->quote($value) : $value;
		return $values;
	}

	private function assignColumns($columns)
	{
		$values = array();
		foreach ($columns as $column => $value)
			$values[] = "$column = " . (!\is_numeric($value) ? $this->quote($value) : $value);
		return $values;
	}
}
