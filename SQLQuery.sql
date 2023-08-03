CREATE TABLE Users(
  
	UsersID INT NOT NULL IDENTITY,  
  	UsersName VARCHAR(64), 
  	UsersEmail VARCHAR(128),
  	UsersPhone VARCHAR(255),
    UsersPassword VARCHAR(255),
  	PRIMARY KEY(UsersID)
);

select * from Users



drop table Users