-- MySQL Script generated by MySQL Workbench
-- Wed Feb  1 20:31:55 2023
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema catalogdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema catalogdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `catalogdb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `catalogdb` ;

-- -----------------------------------------------------
-- Table `catalogdb`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `catalogdb`.`category` (
  `CategoryId` VARCHAR(20) NOT NULL,
  `CategoryName` VARCHAR(30) NOT NULL,
  `CategoryDescription` TEXT NULL DEFAULT NULL,
  PRIMARY KEY (`CategoryId`),
  UNIQUE INDEX `uc_Category_CategoryName` (`CategoryName` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `catalogdb`.`product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `catalogdb`.`product` (
  `ProductId` VARCHAR(20) NOT NULL,
  `ProductName` VARCHAR(35) NOT NULL,
  `CategoryId` VARCHAR(20) NULL DEFAULT NULL,
  `Description` TEXT NULL DEFAULT NULL,
  `Price` DECIMAL(9,2) NOT NULL DEFAULT '0.00',
  `MRP` DECIMAL(9,2) NOT NULL,
  `Discount` DECIMAL(9,2) NULL DEFAULT '0.00',
  `Height` DECIMAL(5,2) NULL DEFAULT '0.00',
  `Depth` DECIMAL(5,2) NULL DEFAULT '0.00',
  `Width` DECIMAL(5,2) NULL DEFAULT '0.00',
  `Image` BLOB NULL DEFAULT NULL,
  PRIMARY KEY (`ProductId`),
  INDEX `fk_Product_Category_CategoryId` (`CategoryId` ASC) VISIBLE,
  CONSTRAINT `fk_Product_Category_CategoryId`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `catalogdb`.`category` (`CategoryId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `catalogdb`.`productstore`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `catalogdb`.`productstore` (
  `ProductStoreId` VARCHAR(25) NOT NULL,
  `Location` VARCHAR(40) NOT NULL,
  `ProductStoreName` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`ProductStoreId`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `catalogdb`.`stock`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `catalogdb`.`stock` (
  `ProductId` VARCHAR(20) NOT NULL,
  `ProductStoreId` VARCHAR(25) NOT NULL,
  `Quantity` INT NOT NULL DEFAULT '0',
  `Unit` VARCHAR(20) NULL DEFAULT NULL,
  PRIMARY KEY (`ProductId`, `ProductStoreId`),
  INDEX `fk_Stock_Product_ProductId` (`ProductId` ASC) VISIBLE,
  INDEX `fk_Stock_ProductStore_ProductStoreId` (`ProductStoreId` ASC) VISIBLE,
  CONSTRAINT `fk_Stock_Product_ProductId`
    FOREIGN KEY (`ProductId`)
    REFERENCES `catalogdb`.`product` (`ProductId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_Stock_ProductStore_ProductStoreId`
    FOREIGN KEY (`ProductStoreId`)
    REFERENCES `catalogdb`.`productstore` (`ProductStoreId`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
