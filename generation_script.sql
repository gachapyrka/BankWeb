create schema if not exists `bank_database`;

use `bank_database`;

create table if not exists `banks`(
	`id` int not null auto_increment,
    `name` varchar(50) not null,
    `hla` float not null,
    `iff` float not null,
    `do` float not null,
    `af30d` float not null,
    `cf30d` float not null,
    `capital` float not null,
    `am1y` float not null,
    `cm1y` float not null,
    `ib` float not null,
    `dl` float not null,
    `ld` float not null,
    `ra` float not null,
    constraint `PK_banks` primary key (`id` ASC)
);

create table if not exists `users`(

	`id` int not null auto_increment,
	`bankId` int not null,
    `login` varchar(50) not null,
    `password` varchar(50) not null,
     constraint `PK_users` primary key (`id` ASC),
     constraint `FK_users_banks` foreign key (`bankId` ASC) references `banks` (id)
);