<?php

define('LIBRARY_PATH', dirname(__FILE__) . '/../../../blackbirdcms/');
require_once LIBRARY_PATH . '/sql/connection.php';

class ConnectionTest extends PHPUnit_Framework_TestCase
{
	const MYSQL_CONNECTION_STRING = 'mysqli://root@localhost/mysql';
	const TEST_CONNECTION_STRING = 'mysqli://root@localhost/test';

	function testConnectWithMySQLi()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::MYSQL_CONNECTION_STRING);
	}

	function testFetchAllCanRetrieveAllRows()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::MYSQL_CONNECTION_STRING);
		$rows = $conn->fetchAll('select * from user');
		$this->assertTrue($rows !== false);
		$this->assertTrue(count($rows) > 0);

		$firstRow = $rows[0];
		$this->assertTrue($firstRow->Host != '');
		$this->assertTrue($firstRow->User != '');
	}

	/**
	 * @todo Implement testFetchAsoc().
	 */
	function testFetchAsoc()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::MYSQL_CONNECTION_STRING);
		$rows = $conn->fetchAssoc('select User, Host from user');
		$this->assertTrue(count($rows) > 0 );
		$this->assertTrue($rows['root'] == 'localhost');
	}

	/**
	 * @todo Implement testFetchColumn().
	 */
	function testFetchColumn()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::MYSQL_CONNECTION_STRING);
		$rows = $conn->fetchAssoc('show tables');
		$this->assertTrue(count($rows) > 0 );
		$this->contains('columns_priv', $rows);
		$this->contains('tables_priv', $rows);
		$this->contains('proc', $rows);
		$this->contains('user', $rows);
	}

	/**
	 * @todo Implement testFetchScalar().
	 */
	function testFetchScalar()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::MYSQL_CONNECTION_STRING);
		$host = $conn->fetchScalar("select Host from user where User = 'root'");
		$this->assertTrue($host == 'localhost');
	}

	function testInsertAndUpdate()
	{
		$conn = BB\SQL\Connection::connect(ConnectionTest::TEST_CONNECTION_STRING);
		$this->assertTrue($conn->execute('drop table if exists test_insert_update'));
		$this->assertTrue($conn->execute('create table test_insert_update ( id int not null primary key auto_increment, one varchar(50), two decimal(9,2) )'));
		$id = $conn->insert('test_insert_update', array('one' => 'hello', 'two' => 3.14));
		$this->assertTrue($id == 1);

		$obj = new \stdClass();
		$obj->one = 'world';
		$obj->two = 2.71;
		$id = $conn->insert('test_insert_update', $obj);
		$this->assertTrue($id == 2);

		$first = $conn->fetchRow('select one, two from test_insert_update where id = 1');
		$this->assertEquals('hello', $first->one);
		$this->assertEquals(3.14, $first->two);

		$second = $conn->fetchRow('select one, two from test_insert_update where id = 2');
		$this->assertEquals('world', $second->one);
		$this->assertEquals(2.71, $second->two);

		$this->assertTrue($conn->update('test_insert_update', array('one' => 'Hello', 'two' => 6.28), 'id = 1'));
		$first = $conn->fetchRow('select one, two from test_insert_update where id = 1');
		$this->assertEquals('Hello', $first->one);
		$this->assertEquals(6.28, $first->two);

		$second->one = 'World';
		$second->two = 3.42;
		$this->assertTrue($conn->update('test_insert_update', $second, 'id = 2'));
		$second = $conn->fetchRow('select one, two from test_insert_update where id = 2');
		$this->assertEquals('World', $second->one);
		$this->assertEquals(3.42, $second->two);

		$conn->execute('drop table if exists test_insert_update');
	}
}

?>
