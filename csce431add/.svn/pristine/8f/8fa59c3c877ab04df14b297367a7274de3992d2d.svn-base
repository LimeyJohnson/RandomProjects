CREATE DATABASE IF NOT EXISTS `gcopley-car_dealership`;
USE `gcopley-car_dealership`;

CREATE TABLE customer ( 
  id     INTEGER    AUTO_INCREMENT, 
  f_name VARCHAR(25), 
  l_name VARCHAR(25), 
  addr_city VARCHAR(30), 
  addr_state CHAR(2), 
  addr_zip CHAR(5), 
  phone  CHAR(10),
  email  VARCHAR(35), 
     PRIMARY KEY ( id )) 
ENGINE = INNODB; 

ALTER TABLE customer 
ADD INDEX( l_name); 

CREATE TABLE dealership ( 
  id         INTEGER    AUTO_INCREMENT, 
  name       VARCHAR(25),
  addr_city  VARCHAR(30)    NOT NULL, 
  addr_state CHAR(2)    NOT NULL, 
  addr_zip   CHAR(5)    NOT NULL, 
     PRIMARY KEY ( id )) 
ENGINE = INNODB; 

CREATE TABLE valid_vehicle_types(
  id         INTEGER    AUTO_INCREMENT, 
  valid      VARCHAR(25),
  PRIMARY KEY(id))
ENGINE = INNODB;

CREATE TABLE vehicle_type ( 
  id           INTEGER    AUTO_INCREMENT, 
  vehicle_type INTEGER    NOT NULL,
  make         VARCHAR(35)    NOT NULL, 
  model        VARCHAR(40)    NOT NULL, 
  vehicle_year CHAR(4)    NOT NULL, 
  mpg          INTEGER,
  doors        INTEGER(4) NOT NULL DEFAULT 4,
  market_price DECIMAL(8,2), 
     PRIMARY KEY ( id ),
     FOREIGN KEY (vehicle_type) references valid_vehicle_types(id)) 
ENGINE = INNODB; 

ALTER TABLE vehicle_type 
ADD INDEX( make); 

ALTER TABLE vehicle_type 
ADD INDEX( model); 

CREATE TABLE vehicle_condition (
  id           INTEGER AUTO_INCREMENT,
  description VARCHAR(255),
  PRIMARY KEY ( id ))
ENGINE = INNODB;

CREATE TABLE color (
  hex_color    CHAR(7),
  description  VARCHAR(20),
  PRIMARY KEY (hex_color))
ENGINE = INNODB;

CREATE TABLE vehicle ( 
  vin          CHAR(17), 
  color        CHAR(7),
  vehicle_type INTEGER NOT NULL, 
  dealership   INTEGER,
  odometer     INTEGER,
  vehicle_condition  INTEGER,
  description VARCHAR(255),
  display_photo VARCHAR(60), 
     PRIMARY KEY ( vin ), 
     FOREIGN KEY ( vehicle_type ) references vehicle_type(id), 
     FOREIGN KEY ( dealership ) references dealership(id) ON DELETE SET NULL,
     FOREIGN KEY (vehicle_condition) references vehicle_condition(id),
     FOREIGN KEY ( color ) references color(hex_color)) 
ENGINE = INNODB; 

CREATE TABLE job_categories ( 
  id           INTEGER    AUTO_INCREMENT, 
  title        VARCHAR(35)    NOT NULL, 
  base_salary  DECIMAL(8,2)    NOT NULL, 
  bonus_salary DECIMAL(8,2),
  quota        DECIMAL(8,2), 
     PRIMARY KEY ( id )) 
ENGINE = INNODB; 

ALTER TABLE job_categories 
ADD INDEX( title); 

CREATE TABLE employee ( 
  id         INTEGER    AUTO_INCREMENT, 
  f_name     VARCHAR(25), 
  l_name     VARCHAR(25), 
  addr_city  VARCHAR(30), 
  addr_state CHAR(2), 
  addr_zip   CHAR(5), 
  job        INTEGER, 
  phone      CHAR(10),
  start_date DATE, 
  end_date   DATE DEFAULT null, 
  dealership INTEGER, 
  username   VARCHAR(25) NOT NULL, 
  pass       VARCHAR(32) NOT NULL, 
  email      VARCHAR(35), 
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( job ) references job_categories(id),
     FOREIGN KEY (dealership) references dealership(id) ON DELETE SET NULL) 
ENGINE = INNODB; 

ALTER TABLE employee 
ADD INDEX( l_name); 

CREATE TABLE messages ( 
  id        INTEGER    AUTO_INCREMENT, 
  from_id   INTEGER    NOT NULL, 
  to_id     INTEGER    NOT NULL, 
  sent_date TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
  message   VARCHAR(511)    NOT NULL, 
  read_count INTEGER NOT NULL DEFAULT 0,
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( from_id ) references employee(id), 
     FOREIGN KEY ( to_id ) references employee(id)) 
ENGINE = INNODB; 

CREATE TABLE vehicle_requests ( 
  id                INTEGER    AUTO_INCREMENT,
  vin               CHAR(17)   NOT NULL,
  to_dealership     INTEGER, 
  request_date      DATETIME   NOT NULL,
  approved          BOOLEAN    NOT NULL DEFAULT 0,
  approved_by_emp   INTEGER    DEFAULT NULL,
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( approved_by_emp ) references employee(id),
     FOREIGN KEY (to_dealership) references dealership(id) ON DELETE SET NULL) 
ENGINE = INNODB; 

CREATE TABLE payroll ( 
  id          INTEGER    AUTO_INCREMENT, 
  employee_id INTEGER    NOT NULL, 
  amount      DECIMAL(8,2)    NOT NULL, 
  pay_date    DATE    NOT NULL, 
  is_paid     BOOLEAN    NOT NULL, 
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( employee_id ) references employee(id)) 
ENGINE = INNODB; 

CREATE TABLE sales (
  id          INTEGER    AUTO_INCREMENT,
  salesman_id INTEGER,
  manager_id  INTEGER,
  customer_id INTEGER NOT NULL,
  vin         CHAR(17)    NOT NULL,
  sale_price  DECIMAL(8,2)    NOT NULL,
  sale_date   DATE    NOT NULL,
  status      VARCHAR(15) DEFAULT "PENDING",
  terms       VARCHAR(255),
  notes       VARCHAR(255),
     PRIMARY KEY ( id ),
     FOREIGN KEY ( salesman_id ) references employee(id),
     FOREIGN KEY ( manager_id ) references employee(id),
     FOREIGN KEY ( customer_id ) references customer(id),
     FOREIGN KEY ( vin ) references vehicle(vin)) 
ENGINE = INNODB;
