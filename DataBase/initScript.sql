create schema core;

create table core.article
(
	article_id serial
		constraint article_pk
			primary key,
	text text not null
);

