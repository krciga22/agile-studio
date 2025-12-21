CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `migration_id` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `product_version` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `pk___ef_migrations_history` PRIMARY KEY (`migration_id`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `projects` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    CONSTRAINT `pk_projects` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20231112043926_Add-Project-Model', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `backlog_item_type_schemas` (
    `id` int NOT NULL AUTO_INCREMENT,
    `project_id` int NOT NULL,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    CONSTRAINT `pk_backlog_item_type_schemas` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_type_schemas_projects_project_id` FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_backlog_item_type_schemas_project_id` ON `backlog_item_type_schemas` (`project_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20231119230214_BacklogItemTypeSchemas', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `backlog_item_type` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `backlog_item_type_schema_id` int NOT NULL,
    CONSTRAINT `pk_backlog_item_type` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_type_backlog_item_type_schemas_backlog_item_type` FOREIGN KEY (`backlog_item_type_schema_id`) REFERENCES `backlog_item_type_schemas` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `backlog_item` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `backlog_item_type_id` int NOT NULL,
    CONSTRAINT `pk_backlog_item` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_backlog_item_type_backlog_item_type_id` FOREIGN KEY (`backlog_item_type_id`) REFERENCES `backlog_item_type` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_backlog_item_backlog_item_type_id` ON `backlog_item` (`backlog_item_type_id`);

CREATE INDEX `ix_backlog_item_type_backlog_item_type_schema_id` ON `backlog_item_type` (`backlog_item_type_schema_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240128012846_BacklogItem-And-BacklogItemType', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` DROP FOREIGN KEY `fk_backlog_item_backlog_item_type_backlog_item_type_id`;

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_backlog_item_type_id` FOREIGN KEY (`backlog_item_type_id`) REFERENCES `backlog_item_type` (`id`) ON DELETE CASCADE;

ALTER TABLE `backlog_item_type` DROP FOREIGN KEY `fk_backlog_item_type_backlog_item_type_schemas_backlog_item_type`;

ALTER TABLE `backlog_item_type` ADD CONSTRAINT `fk_backlog_item_type_backlog_item_type_schema_id` FOREIGN KEY (`backlog_item_type_schema_id`) REFERENCES `backlog_item_type_schemas` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240128030226_Rename-truncated-foreign-keys', '8.0.18');

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

ALTER TABLE `backlog_item_type` DROP FOREIGN KEY `fk_backlog_item_type_backlog_item_type_schema_id`;

ALTER TABLE `backlog_item_type_schemas` DROP FOREIGN KEY `fk_backlog_item_type_schemas_projects_project_id`;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'projects');
ALTER TABLE `projects` DROP PRIMARY KEY;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'backlog_item_type_schemas');
ALTER TABLE `backlog_item_type_schemas` DROP PRIMARY KEY;

ALTER TABLE `projects` RENAME `project`;

ALTER TABLE `backlog_item_type_schemas` RENAME `backlog_item_type_schema`;

ALTER TABLE `backlog_item_type_schema` RENAME INDEX `ix_backlog_item_type_schemas_project_id` TO `ix_backlog_item_type_schema_project_id`;

ALTER TABLE `project` ADD CONSTRAINT `pk_project` PRIMARY KEY (`id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'project', 'id');

ALTER TABLE `backlog_item_type_schema` ADD CONSTRAINT `pk_backlog_item_type_schema` PRIMARY KEY (`id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'backlog_item_type_schema', 'id');

ALTER TABLE `backlog_item_type` ADD CONSTRAINT `fk_backlog_item_type_backlog_item_type_schema_id` FOREIGN KEY (`backlog_item_type_schema_id`) REFERENCES `backlog_item_type_schema` (`id`) ON DELETE CASCADE;

ALTER TABLE `backlog_item_type_schema` ADD CONSTRAINT `fk_backlog_item_type_schema_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240128052249_test', '8.0.18');

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item_type_schema` DROP FOREIGN KEY `fk_backlog_item_type_schema_project_project_id`;

ALTER TABLE `backlog_item_type_schema` DROP INDEX `ix_backlog_item_type_schema_project_id`;

ALTER TABLE `backlog_item_type_schema` DROP COLUMN `project_id`;

ALTER TABLE `project` ADD `backlog_item_type_schema_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_project_backlog_item_type_schema_id` ON `project` (`backlog_item_type_schema_id`);

ALTER TABLE `project` ADD CONSTRAINT `fk_project_backlog_item_type_schema_id` FOREIGN KEY (`backlog_item_type_schema_id`) REFERENCES `backlog_item_type_schema` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240128060209_Reverse-Project-BacklogItemTypeSchema-Reationship', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` ADD `project_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_backlog_item_project_id` ON `backlog_item` (`project_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240129040957_BacklogItem-Project', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `sprints` (
    `id` int NOT NULL AUTO_INCREMENT,
    `sprint_number` int NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `start_date` datetime(6) NULL,
    `end_date` datetime(6) NULL,
    CONSTRAINT `pk_sprints` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240204065553_Create-Sprint-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `sprints` ADD `project_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_sprints_project_id` ON `sprints` (`project_id`);

ALTER TABLE `sprints` ADD CONSTRAINT `fk_sprints_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240204072020_Associate-Sprint-With-Project', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `releases` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `project_id` int NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `start_date` datetime(6) NULL,
    `end_date` datetime(6) NULL,
    CONSTRAINT `pk_releases` PRIMARY KEY (`id`),
    CONSTRAINT `fk_releases_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_releases_project_id` ON `releases` (`project_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240205000716_Create-Release-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` ADD `sprint_id` int NULL;

CREATE INDEX `ix_backlog_item_sprint_id` ON `backlog_item` (`sprint_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_sprints_sprint_id` FOREIGN KEY (`sprint_id`) REFERENCES `sprints` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240211035813_Assign-Sprint-to-Backlog-Item', '8.0.18');

COMMIT;

START TRANSACTION;

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

ALTER TABLE `backlog_item` DROP FOREIGN KEY `fk_backlog_item_sprints_sprint_id`;

ALTER TABLE `releases` DROP FOREIGN KEY `fk_releases_project_project_id`;

ALTER TABLE `sprints` DROP FOREIGN KEY `fk_sprints_project_project_id`;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'sprints');
ALTER TABLE `sprints` DROP PRIMARY KEY;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'releases');
ALTER TABLE `releases` DROP PRIMARY KEY;

ALTER TABLE `sprints` RENAME `sprint`;

ALTER TABLE `releases` RENAME `release`;

ALTER TABLE `sprint` RENAME INDEX `ix_sprints_project_id` TO `ix_sprint_project_id`;

ALTER TABLE `release` RENAME INDEX `ix_releases_project_id` TO `ix_release_project_id`;

ALTER TABLE `sprint` ADD CONSTRAINT `pk_sprint` PRIMARY KEY (`id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'sprint', 'id');

ALTER TABLE `release` ADD CONSTRAINT `pk_release` PRIMARY KEY (`id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'release', 'id');

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_sprint_sprint_id` FOREIGN KEY (`sprint_id`) REFERENCES `sprint` (`id`);

ALTER TABLE `release` ADD CONSTRAINT `fk_release_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE;

ALTER TABLE `sprint` ADD CONSTRAINT `fk_sprint_project_project_id` FOREIGN KEY (`project_id`) REFERENCES `project` (`id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240211064856_Singularize-Sprint-And-Release-Table-Names', '8.0.18');

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` ADD `release_id` int NULL;

CREATE INDEX `ix_backlog_item_release_id` ON `backlog_item` (`release_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_release_release_id` FOREIGN KEY (`release_id`) REFERENCES `release` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240211070237_Assign-Release-to-Backlog-Item', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `workflow` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    CONSTRAINT `pk_workflow` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240217045331_Create-Workflow-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item_type` ADD `workflow_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_backlog_item_type_workflow_id` ON `backlog_item_type` (`workflow_id`);

ALTER TABLE `backlog_item_type` ADD CONSTRAINT `fk_backlog_item_type_workflow_workflow_id` FOREIGN KEY (`workflow_id`) REFERENCES `workflow` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240217054354_Assign-Workflow-To-BacklogItemType', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `workflow_state` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `workflow_id` int NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    CONSTRAINT `pk_workflow_state` PRIMARY KEY (`id`),
    CONSTRAINT `fk_workflow_state_workflow_workflow_id` FOREIGN KEY (`workflow_id`) REFERENCES `workflow` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_workflow_state_workflow_id` ON `workflow_state` (`workflow_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240217060324_Create-WorkflowState-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` ADD `workflow_state_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_backlog_item_workflow_state_id` ON `backlog_item` (`workflow_state_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_workflow_state_id` FOREIGN KEY (`workflow_state_id`) REFERENCES `workflow_state` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240217222121_Assign-WorkflowState-To-BacklogItem', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `user` (
    `id` int NOT NULL AUTO_INCREMENT,
    `email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `first_name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `last_name` longtext CHARACTER SET utf8mb4 NOT NULL,
    `created_on` datetime(6) NOT NULL,
    CONSTRAINT `pk_user` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240223043227_Create-User-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `project` ADD `created_by_id` int NULL;

CREATE INDEX `ix_project_created_by_id` ON `project` (`created_by_id`);

ALTER TABLE `project` ADD CONSTRAINT `fk_project_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240223043802_Assign-CreatedBy-User-to-Project', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `workflow_state` ADD `created_by_id` int NULL;

ALTER TABLE `workflow` ADD `created_by_id` int NULL;

ALTER TABLE `sprint` ADD `created_by_id` int NULL;

ALTER TABLE `release` ADD `created_by_id` int NULL;

ALTER TABLE `backlog_item_type_schema` ADD `created_by_id` int NULL;

ALTER TABLE `backlog_item_type` ADD `created_by_id` int NULL;

ALTER TABLE `backlog_item` ADD `created_by_id` int NULL;

CREATE INDEX `ix_workflow_state_created_by_id` ON `workflow_state` (`created_by_id`);

CREATE INDEX `ix_workflow_created_by_id` ON `workflow` (`created_by_id`);

CREATE INDEX `ix_sprint_created_by_id` ON `sprint` (`created_by_id`);

CREATE INDEX `ix_release_created_by_id` ON `release` (`created_by_id`);

CREATE INDEX `ix_backlog_item_type_schema_created_by_id` ON `backlog_item_type_schema` (`created_by_id`);

CREATE INDEX `ix_backlog_item_type_created_by_id` ON `backlog_item_type` (`created_by_id`);

CREATE INDEX `ix_backlog_item_created_by_id` ON `backlog_item` (`created_by_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `backlog_item_type` ADD CONSTRAINT `fk_backlog_item_type_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `backlog_item_type_schema` ADD CONSTRAINT `fk_backlog_item_type_schema_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `release` ADD CONSTRAINT `fk_release_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `sprint` ADD CONSTRAINT `fk_sprint_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `workflow` ADD CONSTRAINT `fk_workflow_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

ALTER TABLE `workflow_state` ADD CONSTRAINT `fk_workflow_state_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20240224060915_Assign-Created-By-User-to-Other-Entities', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `child_backlog_item_type` (
    `id` int NOT NULL AUTO_INCREMENT,
    `child_type_id` int NOT NULL,
    `parent_type_id` int NOT NULL,
    `schema_id` int NOT NULL,
    `created_on` datetime(6) NOT NULL,
    `created_by_id` int NULL,
    CONSTRAINT `pk_child_backlog_item_type` PRIMARY KEY (`id`),
    CONSTRAINT `fk_child_backlog_item_type_child_type_backlog_item_type_id` FOREIGN KEY (`child_type_id`) REFERENCES `backlog_item_type` (`id`) ON DELETE CASCADE,
    CONSTRAINT `fk_child_backlog_item_type_parent_type_backlog_item_type_id` FOREIGN KEY (`parent_type_id`) REFERENCES `backlog_item_type` (`id`) ON DELETE CASCADE,
    CONSTRAINT `fk_child_backlog_item_type_schema_backlog_item_type_schema_id` FOREIGN KEY (`schema_id`) REFERENCES `backlog_item_type_schema` (`id`) ON DELETE CASCADE,
    CONSTRAINT `fk_child_backlog_item_type_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_child_backlog_item_type_child_type_id` ON `child_backlog_item_type` (`child_type_id`);

CREATE INDEX `ix_child_backlog_item_type_created_by_id` ON `child_backlog_item_type` (`created_by_id`);

CREATE INDEX `ix_child_backlog_item_type_parent_type_id` ON `child_backlog_item_type` (`parent_type_id`);

CREATE INDEX `ix_child_backlog_item_type_schema_id` ON `child_backlog_item_type` (`schema_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20241013174852_Child-Backlog-item-Type', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `backlog_item` ADD `parent_backlog_item_id` int NULL;

CREATE INDEX `ix_backlog_item_parent_backlog_item_id` ON `backlog_item` (`parent_backlog_item_id`);

ALTER TABLE `backlog_item` ADD CONSTRAINT `fk_backlog_item_parent_backlog_item_id` FOREIGN KEY (`parent_backlog_item_id`) REFERENCES `backlog_item` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20241116020649_Parent-Backlog-Item', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `backlog_item_link_type` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `title_opposite` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `created_by_id` int NULL,
    CONSTRAINT `pk_backlog_item_link_type` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_link_type_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_backlog_item_link_type_created_by_id` ON `backlog_item_link_type` (`created_by_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20250323020106_Create-Backlog-Item-Link-Type-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `backlog_item_link_type_schema` (
    `id` int NOT NULL AUTO_INCREMENT,
    `title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `description` longtext CHARACTER SET utf8mb4 NULL,
    `created_on` datetime(6) NOT NULL,
    `created_by_id` int NULL,
    CONSTRAINT `pk_backlog_item_link_type_schema` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_link_type_schema_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_backlog_item_link_type_schema_created_by_id` ON `backlog_item_link_type_schema` (`created_by_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20250326024911_Create-Backlog-Item-Link-Type-Schema-Entity', '8.0.18');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20250411223853_V4-Updates', '8.0.18');

COMMIT;

START TRANSACTION;

ALTER TABLE `project` ADD `backlog_item_link_type_schema_id` int NOT NULL DEFAULT 0;

CREATE INDEX `ix_project_backlog_item_link_type_schema_id` ON `project` (`backlog_item_link_type_schema_id`);

ALTER TABLE `project` ADD CONSTRAINT `fk_project_backlog_item_link_type_schema_id` FOREIGN KEY (`backlog_item_link_type_schema_id`) REFERENCES `backlog_item_link_type_schema` (`id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20250411224413_Assign-Backlog-Item-Link-Type-Schema-to-Project', '8.0.18');

COMMIT;

START TRANSACTION;

CREATE TABLE `backlog_item_link_type_schema_entry` (
    `id` int NOT NULL AUTO_INCREMENT,
    `backlog_item_link_type_schema_id` int NOT NULL,
    `backlog_item_link_type_id` int NOT NULL,
    `created_on` datetime(6) NOT NULL,
    `created_by_id` int NULL,
    CONSTRAINT `pk_backlog_item_link_type_schema_entry` PRIMARY KEY (`id`),
    CONSTRAINT `fk_backlog_item_link_type_schema_entry_link_type_id` FOREIGN KEY (`backlog_item_link_type_id`) REFERENCES `backlog_item_link_type` (`id`) ON DELETE CASCADE,
    CONSTRAINT `fk_backlog_item_link_type_schema_entry_link_type_schema_id` FOREIGN KEY (`backlog_item_link_type_schema_id`) REFERENCES `backlog_item_link_type_schema` (`id`) ON DELETE CASCADE,
    CONSTRAINT `fk_backlog_item_link_type_schema_entry_user_created_by_id` FOREIGN KEY (`created_by_id`) REFERENCES `user` (`id`)
) CHARACTER SET=utf8mb4;

CREATE INDEX `ix_backlog_item_link_type_schema_entry_created_by_id` ON `backlog_item_link_type_schema_entry` (`created_by_id`);

CREATE INDEX `ix_backlog_item_link_type_schema_entry_link_type_id` ON `backlog_item_link_type_schema_entry` (`backlog_item_link_type_id`);

CREATE INDEX `ix_backlog_item_link_type_schema_entry_link_type_schema_id` ON `backlog_item_link_type_schema_entry` (`backlog_item_link_type_schema_id`);

CREATE UNIQUE INDEX `ix_backlog_item_link_type_schema_entry_unique` ON `backlog_item_link_type_schema_entry` (`backlog_item_link_type_schema_id`, `backlog_item_link_type_id`);

INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
VALUES ('20250412004030_Create-Backlog-Item-Link-Type-Schema-Entry-Entity', '8.0.18');

COMMIT;

