<?php

namespace BB\SQL;

define('LIBRARY_PATH', dirname(__FILE__) . '/../../../blackbirdcms/');
require_once dirname(__FILE__) . '/../../../blackbirdcms/sql/connection.php';

class ConnectionTest extends \PHPUnit_Framework_TestCase
{
	const CONNECTION_STRING = 'mysqli://root@localhost/mysql';

	public function testConnectWithMySQLi()
	{
		$conn = Connection::connect(ConnectionTest::CONNECTION_STRING);
	}

	public function testFetchAllCanRetrieveAllRows()
	{
		$conn = Connection::connect(ConnectionTest::CONNECTION_STRING);
		$rows = $conn->fetchAll('select * from user');var_dump($rows);
		$this->assertTrue($rows !== false);
		$this->assertTrue(count($rows) > 0);

		$firstRow = $rows[0];var_dump($rows);
		$this->assertTrue($firstRow->Host != '');
		$this->assertTrue($firstRow->User != '');
	}

	/**
	 * @todo Implement testFetchAsoc().
	 */
	public function testFetchAsoc()
	{
		// Remove the following lines when you implement this test.
		$this->markTestIncomplete(
			'This test has not been implemented yet.'
		);
	}

	/**
	 * @todo Implement testFetchColumn().
	 */
	public function testFetchColumn()
	{
		// Remove the following lines when you implement this test.
		$this->markTestIncomplete(
			'This test has not been implemented yet.'
		);
	}

	/**
	 * @todo Implement testFetchScalar().
	 */
	public function testFetchScalar()
	{
		// Remove the following lines when you implement this test.
		$this->markTestIncomplete(
			'This test has not been implemented yet.'
		);
	}
}

?>
