CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS jobs(
  id INT primary key AUTO_INCREMENT NOT NULL COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'time created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'time updated',
  name VARCHAR(255) NOT NULL COMMENT 'job name',
  creatorId VARCHAR(255) NOT NULL COMMENT 'FK: user account',
  description VARCHAR(255) NOT NULL COMMENT 'job desc.',
  qoute INT NOT NULL COMMENT 'job qoute',
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS contractors(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'time created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'time updated',
  accountId VARCHAR(255) NOT NULL COMMENT 'FK: user account Id',
  jobId INT NOT NULL COMMENT 'FK: job',
  role VARCHAR(255) DEFAULT "contractor" COMMENT 'user role in jobs',
  FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (jobId) REFERENCES jobs(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';