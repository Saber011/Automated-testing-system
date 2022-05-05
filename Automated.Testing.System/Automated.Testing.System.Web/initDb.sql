create database test
    with owner postgres;
create schema core;
create table if not exists core.article
(
    article_id serial not null
        constraint article_pk
            primary key,
    text text not null,
    title varchar(120) not null
);

alter table core.article owner to postgres;

create table if not exists core.role
(
    role_id serial not null
        constraint role_pk
            primary key,
    name varchar(50) not null
);

alter table core.role owner to postgres;

create unique index if not exists role_name_uindex
    on core.role (name);

create table if not exists core."user"
(
    user_id serial not null
        constraint user_pk
            primary key,
    login varchar(120) not null,
    password text not null,
    is_deleted integer
);

alter table core."user" owner to postgres;

create unique index if not exists user_login_uindex
    on core."user" (login);

create table if not exists core.category
(
    category_id serial not null
        constraint category_pk
            primary key,
    name varchar(120) not null
);

alter table core.category owner to postgres;

create unique index if not exists category_name_uindex
    on core.category (name);

create table if not exists core.test
(
    test_id serial not null
        constraint test_pk
            primary key,
    name varchar(120) not null,
    user_id integer not null
        constraint test_user_user_id_fk
            references core."user",
    is_deleted integer,
    create_date date default now()
);

alter table core.test owner to postgres;

create table if not exists core.test_type
(
    test_type_id serial not null
        constraint test_type_pk
            primary key,
    name varchar(120) not null
);

alter table core.test_type owner to postgres;

create unique index if not exists test_type_name_uindex
    on core.test_type (name);

create table if not exists core.test_task
(
    test_task_id serial not null
        constraint test_task_pk
            primary key,
    description text not null,
    test_id integer not null
        constraint test_task_test_test_id_fk
            references core.test,
    test_type_id integer
        constraint test_task_test_type_test_type_id_fk
            references core.test_type,
    is_deleted integer
);

alter table core.test_task owner to postgres;

create table if not exists core.ref_test_category
(
    test_id integer
        constraint ref_test_category_test_test_id_fk
            references core.test,
    category_id integer
        constraint ref_test_category_category_category_id_fk
            references core.category
);

alter table core.ref_test_category owner to postgres;

create table if not exists core.ref_article_category
(
    article_id integer
        constraint ref_article_category_article_article_id_fk
            references core.article,
    category_id integer
        constraint ref_article_category_category_category_id_fk
            references core.category
);

alter table core.ref_article_category owner to postgres;

create table if not exists core.user_roles
(
    user_id integer
        constraint user_roles_user_user_id_fk
            references core."user",
    role_id integer
        constraint user_roles_role_role_id_fk
            references core.role
);

alter table core.user_roles owner to postgres;

create table if not exists core.user_authorization_provider
(
    user_authorization_provider_id serial not null
        constraint user_authorization_provider_pk
            primary key,
    user_id integer
        constraint user_authorization_provider_user_user_id_fk
            references core."user",
    token text
);

alter table core.user_authorization_provider owner to postgres;

create table if not exists core.user_token
(
    user_id integer
        constraint user_token_user_user_id_fk
            references core."user",
    token text,
    expires date,
    created date,
    created_by_ip text,
    revoked date,
    revoked_by_ip text,
    replaced_by_token text
);

alter table core.user_token owner to postgres;

create table if not exists core.test_task_response_option
(
    test_task_response_option_id serial not null
        constraint test_task_response_option_pk
            primary key,
    response_option text,
    test_task_id integer
        constraint test_task_response_option_test_task_test_task_id_fk
            references core.test_task
);

alter table core.test_task_response_option owner to postgres;

create table if not exists core.test_result
(
    test_result_id serial not null
        constraint test_result_pk
            primary key,
    user_id integer
        constraint test_result_user_user_id_fk
            references core."user",
    test_id integer
        constraint test_result_test_test_id_fk
            references core.test,
    test_task_id integer
        constraint test_result_test_task_test_task_id_fk
            references core.test_task,
    user_answer text,
    correct_answer text,
    user_response_is_correct boolean,
    execute_date timestamp default (now())::timestamp without time zone,
    try_execute integer
);

alter table core.test_result owner to postgres;

create table if not exists core.test_task_answer
(
    test_task_answer_id serial not null
        constraint test_task_answer_pk
            primary key,
    test_task_id integer
        constraint test_task_answer_test_task_test_task_id_fk
            references core.test_task,
    answer text
);

alter table core.test_task_answer owner to postgres;

create table if not exists core.dictionary
(
    dictionary_id serial not null
        constraint dictionary_pk
            primary key,
    dictionary_name varchar(120) not null,
    table_name varchar(120) not null
);

alter table core.dictionary owner to postgres;

create unique index if not exists dictionary_dictionary_name_uindex
    on core.dictionary (dictionary_name);

INSERT INTO core.role (role_id, name) VALUES (DEFAULT, 'User');
INSERT INTO core.role (role_id, name) VALUES (DEFAULT, 'Admin');

INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'Категории', 'category');
INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'Роли', 'role');
INSERT INTO core.dictionary (dictionary_id, dictionary_name, table_name) VALUES (DEFAULT, 'Статьи', 'article');

create or replace function insert_user_role() returns trigger
    language plpgsql
as $$
BEGIN
    INSERT INTO core.user_roles(user_id, role_id)
    VALUES(NEW.user_id, 1);

    RETURN NEW;
END;
$$;

alter function insert_user_role() owner to postgres;

create trigger user_role_trigger
    after insert
    on core."user"
    for each row
execute procedure insert_user_role();