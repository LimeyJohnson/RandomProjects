USE `gcopley-car_dealership`;

CREATE TABLE customer2 ( 
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

ALTER TABLE customer2 
ADD INDEX( l_name); 
INSERT INTO customer2 (SELECT * from customer);

CREATE TABLE dealership2 ( 
  id         INTEGER    AUTO_INCREMENT, 
  name       VARCHAR(25),
  addr_city  VARCHAR(30)    NOT NULL, 
  addr_state CHAR(2)    NOT NULL, 
  addr_zip   CHAR(5)    NOT NULL, 
     PRIMARY KEY ( id )) 
ENGINE = INNODB; 
INSERT INTO dealership2 (SELECT * from dealership);

CREATE TABLE valid_vehicle_types2(
  id         INTEGER    AUTO_INCREMENT, 
  valid      VARCHAR(25),
  PRIMARY KEY(id))
ENGINE = INNODB;

INSERT INTO valid_vehicle_types2 (SELECT * from valid_vehicle_types);

CREATE TABLE vehicle_type2 ( 
  id           INTEGER    AUTO_INCREMENT, 
  vehicle_type INTEGER    NOT NULL,
  make         VARCHAR(35)    NOT NULL, 
  model        VARCHAR(40)    NOT NULL, 
  vehicle_year CHAR(4)    NOT NULL, 
  mpg          INTEGER,
  doors        INTEGER(4) NOT NULL DEFAULT 4,
  market_price DECIMAL(8,2), 
     PRIMARY KEY ( id ),
     FOREIGN KEY (vehicle_type) references valid_vehicle_types2(id)) 
ENGINE = INNODB; 

ALTER TABLE vehicle_type2
ADD INDEX( make); 

ALTER TABLE vehicle_type2
ADD INDEX( model); 
INSERT INTO vehicle_type2 (SELECT * from vehicle_type);

CREATE TABLE vehicle_condition2 (
  id           INTEGER AUTO_INCREMENT,
  description VARCHAR(255),
  PRIMARY KEY ( id ))
ENGINE = INNODB;

INSERT INTO vehicle_condition2 (SELECT * from vehicle_condition);

CREATE TABLE color2 (
  hex_color    CHAR(7),
  description  VARCHAR(20),
  PRIMARY KEY (hex_color))
ENGINE = INNODB;

INSERT INTO color2 (SELECT * from color);

CREATE TABLE vehicle2 ( 
  vin          CHAR(17), 
  color        CHAR(7),
  vehicle_type INTEGER NOT NULL, 
  dealership   INTEGER,
  odometer     INTEGER,
  vehicle_condition  INTEGER,
  description VARCHAR(255),
  display_photo VARCHAR(60), 
     PRIMARY KEY ( vin ), 
     FOREIGN KEY ( vehicle_type) references vehicle_type2(id), 
     FOREIGN KEY ( dealership ) references dealership2(id) ON DELETE SET NULL,
     FOREIGN KEY (vehicle_condition) references vehicle_condition2(id),
     FOREIGN KEY ( color ) references color2(hex_color)) 
ENGINE = INNODB; 

INSERT INTO vehicle2 (SELECT * from vehicle);

CREATE TABLE job_categories2 ( 
  id           INTEGER    AUTO_INCREMENT, 
  title        VARCHAR(35)    NOT NULL, 
  base_salary  DECIMAL(8,2)    NOT NULL, 
  bonus_salary DECIMAL(8,2),
  quota        DECIMAL(8,2), 
     PRIMARY KEY ( id )) 
ENGINE = INNODB; 

ALTER TABLE job_categories2
ADD INDEX( title); 
INSERT INTO job_categories2 (SELECT * from job_categories);

CREATE TABLE employee2 ( 
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
     FOREIGN KEY ( job ) references job_categories2(id),
     FOREIGN KEY (dealership) references dealership2(id) ON DELETE SET NULL) 
ENGINE = INNODB; 

ALTER TABLE employee2
ADD INDEX( l_name); 
INSERT INTO employee2 (SELECT * FROM employee);

CREATE TABLE messages2 ( 
  id        INTEGER    AUTO_INCREMENT, 
  from_id   INTEGER    NOT NULL, 
  to_id     INTEGER    NOT NULL, 
  sent_date TIMESTAMP   NOT NULL DEFAULT CURRENT_TIMESTAMP,
  message   VARCHAR(511)    NOT NULL, 
  read_count INTEGER NOT NULL DEFAULT 0,
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( from_id ) references employee2(id), 
     FOREIGN KEY ( to_id ) references employee2(id)) 
ENGINE = INNODB; 

INSERT INTO messages2 (SELECT * FROM messages);

CREATE TABLE vehicle_requests2 ( 
  id                INTEGER    AUTO_INCREMENT,
  vin               CHAR(17)   NOT NULL,
  to_dealership     INTEGER, 
  request_date      DATETIME   NOT NULL,
  approved          BOOLEAN    NOT NULL DEFAULT 0,
  approved_by_emp   INTEGER    DEFAULT NULL,
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( approved_by_emp ) references employee2(id),
     FOREIGN KEY (to_dealership) references dealership2(id) ON DELETE SET NULL) 
ENGINE = INNODB; 

INSERT INTO vehicle_requests2 (SELECT * from vehicle_requests);

CREATE TABLE payroll2 ( 
  id          INTEGER    AUTO_INCREMENT, 
  employee_id INTEGER    NOT NULL, 
  amount      DECIMAL(8,2)    NOT NULL, 
  pay_date    DATE    NOT NULL, 
  is_paid     BOOLEAN    NOT NULL, 
     PRIMARY KEY ( id ), 
     FOREIGN KEY ( employee_id ) references employee2(id)) 
ENGINE = INNODB; 

INSERT INTO payroll2 (SELECT * from payroll);

CREATE TABLE sales2 (
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
     FOREIGN KEY ( salesman_id ) references employee2(id),
     FOREIGN KEY ( manager_id ) references employee2(id),
     FOREIGN KEY ( customer_id ) references customer2(id),
     FOREIGN KEY ( vin ) references vehicle2(vin)) 
ENGINE = INNODB;

INSERT INTO sales2 (SELECT * from sales);
