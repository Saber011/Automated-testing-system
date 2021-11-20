create schema core;

create table core.article
(
	article_id serial
		constraint article_pk
			primary key,
	text text not null
);

create table core.role
(
	role_id int
		constraint role_pk
			primary key,
	name varchar(50) not null
);

create unique index role_name_uindex
	on core.role (name);

create table core.user
(
	user_id int
		constraint user_pk
			primary key,
	login varchar(120) not null,
	password text not null
);

create unique index user_login_uindex
	on core.user (login);

create table core.category
(
	category_id serial
		constraint category_pk
			primary key,
	name varchar(120) not null
);

create unique index category_name_uindex
	on core.category (name);


create table core.test
(
	test_id serial
		constraint test_pk
			primary key,
	name varchar(120) not null,
	category_id int
		constraint test_category_category_id_fk
			references core.category
);

create table core.test_type
(
	test_type_id serial
		constraint test_type_pk
			primary key,
	name varchar(120) not null
);

create unique index test_type_name_uindex
	on core.test_type (name);

create table core.test_task
(
	test_task_id serial
		constraint test_task_pk
			primary key,
	description text not null,
	test_id int not null
		constraint test_task_test_test_id_fk
			references core.test,
	answers text[],
	response_options text[],
	test_type_id int
		constraint test_task_test_type_test_type_id_fk
			references core.test_type
);


create table core.ref_test_category
(
	test_id int
		constraint ref_test_category_test_test_id_fk
			references core.test,
	category_id int
		constraint ref_test_category_category_category_id_fk
			references core.category
);

create table core.ref_article_category
(
	article_id int
		constraint ref_article_category_article_article_id_fk
			references core.article,
	category_id int
		constraint ref_article_category_category_category_id_fk
			references core.category
);

create table core.user_roles
(
	user_id int
		constraint user_roles_user_user_id_fk
			references core."user",
	role_id int
		constraint user_roles_role_role_id_fk
			references core.role
);

create table core.test_result
(
	test_result_id serial
		constraint test_result_pk
			primary key,
	user_id int
		constraint test_result_user_user_id_fk
			references core."user",
	test_id int
		constraint test_result_test_test_id_fk
			references core.test,
	correct_answers text[],
	incorrect_answers text[]
);

create table core.user_authorization_provider
(
	user_authorization_provider_id serial
		constraint user_authorization_provider_pk
			primary key,
	user_id int
		constraint user_authorization_provider_user_user_id_fk
			references core."user",
	token text
);

create sequence core.user_user_id_seq;

alter table core."user" alter column user_id set default nextval('core.user_user_id_seq');

alter sequence core.user_user_id_seq owned by core."user".user_id;


create table core."user_token"
(
	user_id int
		constraint "user_token_user_user_id_fk"
			references core."user",
	token text
);

alter table core.user_token
	add expires date;

alter table core.user_token
	add created date;

alter table core.user_token
	add created_by_ip text;

alter table core.user_token
	add revoked date;

alter table core.user_token
	add revoked_by_ip text;

alter table core.user_token
	add replaced_by_token text;

create table core.test_task_response_option
(
	test_task_response_option serial
		constraint test_task_response_option_pk
			primary key,
	response_option text,
	test_task_id int
		constraint test_task_response_option_test_task_test_task_id_fk
			references core.test_task
);

alter table core.test_task_response_option rename column test_task_response_option to test_task_response_option_id;

alter table core.test_task rename column response_options to test_task_response_option_id;

alter table core.test_task alter column test_task_response_option_id type int using test_task_response_option_id::int;

alter table core.test_task
	add constraint test_task_test_task_response_option_test_task_response_option_id_fk
		foreign key (test_task_response_option_id) references core.test_task_response_option;

create table core.test_task_answer
(
	test_task_answer_id int
		constraint test_task_answer_pk
			primary key,
	test_task_id int
		constraint test_task_answer_test_task_test_task_id_fk
			references core.test_task,
	answer text
);

alter table core.test_task rename column answers to test_task_answer_id;

alter table core.test_task alter column test_task_answer_id type integer using test_task_answer_id::integer;

alter table core.test_task alter column test_task_response_option_id type integer using test_task_response_option_id::integer;

alter table core.test_task
	add constraint test_task_test_task_answer_test_task_answer_id_fk
		foreign key (test_task_answer_id) references core.test_task_answer;

alter table core.test_task
	add constraint test_task_test_task_response_option_test_task_response_option_id_fk
		foreign key (test_task_response_option_id) references core.test_task_response_option;

alter table core.test_result rename column correct_answers to correct_answers_id;

alter table core.test_result alter column correct_answers_id type integer using correct_answers_id::integer;

alter table core.test_result drop column incorrect_answers;

alter table core.test_result
	add constraint test_result_test_task_answer_test_task_answer_id_fk
		foreign key (correct_answers_id) references core.test_task_answer;

alter table core.test_task drop column test_task_answer_id;

alter table core.test_task
	add test_task_answer_id integer;

alter table core.test_task drop column test_task_response_option_id;

alter table core.test_task
	add test_task_response_option_id integer;

alter table core.test_task
	add constraint test_task_test_task_answer_test_task_answer_id_fk
		foreign key (test_task_answer_id) references core.test_task_answer;

alter table core.test_task
	add constraint test_task_test_task_response_option_test_task_response_option_id_fk
		foreign key (test_task_response_option_id) references core.test_task_response_option;

alter table core.test_result drop column correct_answers_id;
alter table core.test_result
	add correct_answers_id integer;

alter table core.test_result drop column incorrect_answers;

alter table core.test_result
	add constraint test_result_test_task_answer_test_task_answer_id_fk
		foreign key (correct_answers_id) references core.test_task_answer;

alter table core.test_result rename column correct_answers_id to test_task_response_option;

alter table core.test_result drop constraint test_result_test_task_answer_test_task_answer_id_fk;

alter table core.test_result
	add constraint test_result_test_task_response_option_test_task_response_option_id_fk
		foreign key (test_task_response_option) references core.test_task_response_option;

create table core.dictionary
(
	dictionary_id serial
		constraint dictionary_pk
			primary key,
	dictionary_name varchar(120) not null
);

create unique index dictionary_dictionary_name_uindex
	on core.dictionary (dictionary_name);

alter table core.dictionary
	add table_name varchar(120) not null;


create sequence core.role_role_id_seq;

alter table core.role alter column role_id set default nextval('core.role_role_id_seq');

alter sequence core.role_role_id_seq owned by core.role.role_id;



INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'category', 'category');
INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'role', 'role');
INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'article', 'article');