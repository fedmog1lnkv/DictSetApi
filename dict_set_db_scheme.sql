CREATE DATABASE `dict_set`;

USE dict_set;

CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(30) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(50) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `email` (`email`)
);

CREATE TABLE `tokens` (
	`token_id` INT NOT NULL AUTO_INCREMENT,
	`user_id` INT NOT NULL,
	`token` varchar(255),
	PRIMARY KEY (`token_id`),
  UNIQUE KEY `user_id` (`user_id`),
  FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE `sets` (
	`set_id` INT NOT NULL AUTO_INCREMENT,
	`user_id` INT,
	`name` VARCHAR(50),
	`description` TEXT,
	PRIMARY KEY (`set_id`),
  FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE `words` (
	`word_id` INT NOT NULL AUTO_INCREMENT,
	`set_id` INT,
	`word` VARCHAR(100),
	`translate` VARCHAR(100),
	`description` TEXT DEFAULT NULL,
	PRIMARY KEY (`word_id`),
  FOREIGN KEY (set_id) REFERENCES sets (set_id)
);
